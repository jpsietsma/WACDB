<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Map.aspx.cs" Inherits="Map" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="http://serverapi.arcgisonline.com/jsapi/arcgis/2.0/js/dojo/dijit/themes/tundra/tundra.css" />
    <script type="text/javascript" src="http://serverapi.arcgisonline.com/jsapi/arcgis/?v=2.0"></script>
    <script type="text/javascript">
        dojo.require("esri.map");
        dojo.require("esri.tasks.locator");

        var map, locator;

        var mapStreet, mapImagery, mapTopo, mapBoundaries;

        function init() {
            try {
                var initExtent = new esri.geometry.Extent({ "xmin": -8946113, "ymin": 4915512, "xmax": -7845420, "ymax": 5649307, "spatialReference": { "wkid": 102100} });
            map = new esri.Map("map", { extent: initExtent });

            mapStreet = new esri.layers.ArcGISTiledMapServiceLayer("http://server.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer", { id: "mapStreet" });
            map.addLayer(mapStreet);

            mapImagery = initLayer("http://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer", "mapImagery");
            mapTopo = initLayer("http://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer", "mapTopo");
            mapBoundaries = initLayer("http://server.arcgisonline.com/ArcGIS/rest/services/Reference/World_Boundaries_and_Places/MapServer", "mapBoundaries");

            locator = new esri.tasks.Locator("http://sampleserver1.arcgisonline.com/ArcGIS/rest/services/Locators/ESRI_Geocode_USA/GeocodeServer");
            dojo.connect(locator, "onAddressToLocationsComplete", showResults);
            locate(); 
            }
            catch (err) {
                dojo.byId("layers").style.display = "none";
                dojo.byId("reload").style.display = "";
            }
        }

        function initLayer(url, id) {
            var layer = new esri.layers.ArcGISTiledMapServiceLayer(url, {id:id, visible:false});
            map.addLayer(layer);
            return layer;
        }


        function changeMap(layers) {
            try {
                hideImageTiledLayers(layers);
                for (var i = 0; i < layers.length; i++) {
                    layers[i].show();
                }
            }
            catch (err) {
                dojo.byId("layers").style.display = "none";
                dojo.byId("reload").style.display = "";
            }
        }

        function hideImageTiledLayers(layers) {
            for (var j=0, jl=map.layerIds.length; j<jl; j++) {
              var layer = map.getLayer(map.layerIds[j]);
              if (dojo.indexOf(layers, layer) == -1) {
                layer.hide();
              }
            }
        }

        function locate() {
            //map.graphics.clear();
            var add = dojo.byId("hf").value.split(",");
            var address = {
                Address: add[0],
                City: add[1],
                State: add[2],
                Zip: add[3]
            };
            locator.addressToLocations(address, ["Loc_name"]);
        }

        function showResults(candidates) {
            var candidate;
            var symbol = new esri.symbol.SimpleMarkerSymbol();
            var infoTemplate = new esri.InfoTemplate("Location", "Address: ${address}<br />Score: ${score}<br />Source locator: ${locatorName}");

            symbol.setStyle(esri.symbol.SimpleMarkerSymbol.STYLE_DIAMOND);
            symbol.setColor(new dojo.Color([255, 0, 0, 0.75]));

            var points = new esri.geometry.Multipoint(map.spatialReference);

            for (var i = 0, il = candidates.length; i < il; i++) {
                candidate = candidates[i];
                if (candidate.score > 70) {
                    var attributes = { address: candidate.address, score: candidate.score, locatorName: candidate.attributes.Loc_name };
                    var geom = esri.geometry.geographicToWebMercator(candidate.location);
                    var graphic = new esri.Graphic(geom, symbol, attributes, infoTemplate);
                    map.graphics.add(graphic);

                    var textSymbol = new esri.symbol.TextSymbol(attributes.address).setColor(new dojo.Color([128, 0, 0])).setAlign(esri.symbol.Font.ALIGN_START).setFont(new esri.symbol.Font("12pt").setWeight(esri.symbol.Font.WEIGHT_BOLD));
                    //map.graphics.add(new esri.Graphic(geom, new esri.symbol.TextSymbol(attributes.address).setOffset(0, 8)));
                    map.graphics.add(new esri.Graphic(geom, textSymbol));
                    points.addPoint(geom);
                    break;
                }
            }
            map.setExtent(points.getExtent().expand(20));
            setTimeout('map.setLevel(15)', 1000);
        }
        dojo.addOnLoad(init);
    </script>
</head>
<body class="tundra">
    <form id="form1" runat="server">
    <div style="width:100%; height:600px; background-color:#F9F9F9;">
        <asp:HiddenField ID="hf" runat="server" />
        <div id="map" style="width:99%; height:99%; border:1px solid #000; margin:auto;"></div>
        <div id="layers" style="position:absolute; right:10px; top:10px; z-Index:999; padding:3px; border:solid 1px #000000; background-color:#FFFFFF; filter:alpha(opacity=60); font-weight:bold;">
            <%--<button dojoType="dijit.form.Button" onClick="changeMap([mapImagery]);">Imagery</button>
            <button dojoType="dijit.form.Button" onClick="changeMap([mapImagery,mapBoundaries]);">Imagery/Places</button>
            <button dojoType="dijit.form.Button" onClick="changeMap([mapStreet]);">Street Map</button>
            <button dojoType="dijit.form.Button" onClick="changeMap([mapTopo]);">Topographic Map</button>--%>
            <input type="radio" dojoType="dijit.form.RadioButton" name="MapSwitch" onclick="changeMap([mapStreet]);" checked /> Street Map
            <input type="radio" dojoType="dijit.form.RadioButton" name="MapSwitch" onclick="changeMap([mapTopo]);" /> Topo Map
            <input type="radio" dojoType="dijit.form.RadioButton" name="MapSwitch" onclick="changeMap([mapImagery]);" /> Imagery
            <input type="radio" dojoType="dijit.form.RadioButton" name="MapSwitch" onclick="changeMap([mapImagery,mapBoundaries]);" /> Imagery/Places
          </div>
        <div id="reload" style="position:absolute; right:10px; top:10px; z-Index:999; display:none;">
            <button dojoType="dijit.form.Button" onclick="location.reload(true);">Reload Map</button>
        </div>
    </div>
    </form>
</body>
</html>
