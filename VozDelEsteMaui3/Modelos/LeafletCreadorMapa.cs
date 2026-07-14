using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VozDelEsteMaui3.Modelos
{
    public class LeafletCreadorMapa
    {
        public string BuildMapHtml(LeafletMapMode modo, double? lat = null, double? lng = null, string popupTexto = null)
        {
            var centerLat = lat ?? -34.9;
            var centerLng = lng ?? -54.95;
            var zoom = 13;

            var markerScript = (modo == LeafletMapMode.Visualizar || modo == LeafletMapMode.Editar) && lat.HasValue && lng.HasValue
                ? "currentMarker = L.marker([" + lat + ", " + lng + "])" +
                  ".addTo(map)" +
                  ".bindPopup('" + (popupTexto ?? "Ubicación") + "')" +
                  ".openPopup();"
                : "";

            var clickScript = (modo == LeafletMapMode.Seleccionar || modo == LeafletMapMode.Editar)
                ? @"
map.on('click', function(e) {
  if (currentMarker) map.removeLayer(currentMarker);
  currentMarker = L.marker(e.latlng)
    .addTo(map)
    .bindPopup('Ubicación seleccionada: ' + e.latlng.toString())
    .openPopup();
  window.location.href = 'leaflet://location?lat=' + e.latlng.lat + '&lng=' + e.latlng.lng;
});"
                : "";

            return $@"<!DOCTYPE html>
<html>
<head>
  <meta charset='utf-8' />
  <title>Leaflet Map</title>
  <link rel='stylesheet' href='https://unpkg.com/leaflet/dist/leaflet.css' />
  <style>#map {{ height: 100vh; width: 100vw; }}</style>
</head>
<body>
  <div id='map'></div>
  <script src='https://unpkg.com/leaflet/dist/leaflet.js'></script>
  <script>
    var map = L.map('map').setView([{centerLat}, {centerLng}], {zoom});
    L.tileLayer('https://{{s}}.tile.openstreetmap.org/{{z}}/{{x}}/{{y}}.png', {{
      attribution: '© OpenStreetMap contributors'
    }}).addTo(map);
    let currentMarker = null;
    {markerScript}
    {clickScript}
  </script>
</body>
</html>";
        }
    }
    public enum LeafletMapMode
    {
        Visualizar,
        Seleccionar,
        Editar
    }

}
