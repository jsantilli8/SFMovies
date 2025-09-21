<template>
  <div class="movie-map-container">
    <div id="map" class="map"></div>
    <div class="map-controls">
      <button @click="showAllLocations" class="control-btn">Show All Locations</button>
      <button @click="centerOnSF" class="control-btn">Center on SF</button>
      <button @click="toggleSatellite" class="control-btn">{{ isSatellite ? 'Street View' : 'Satellite View' }}</button>
    </div>
  </div>
</template>

<script>
import { onMounted, onUnmounted, ref } from 'vue'
import { useMoviesStore } from '../store/movies'

export default {
  name: 'MovieMap',
  setup() {
    const moviesStore = useMoviesStore()
    let map = null
    let markers = []
    let currentLayer = null
    const isSatellite = ref(false)

    onMounted(() => {
      initMap()
    })

    onUnmounted(() => {
      if (map) {
        map.remove()
      }
    })

    const initMap = () => {
      // Import Leaflet dynamically
      import('leaflet').then(L => {
        // Initialize map centered on San Francisco
        map = L.map('map').setView([37.7749, -122.4194], 12)

        // Add default tile layer
        currentLayer = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
          attribution: '© OpenStreetMap contributors'
        }).addTo(map)

        // Add markers for all locations
        addLocationMarkers()
      })
    }

    const addLocationMarkers = () => {
      if (!map) return

      // Clear existing markers
      markers.forEach(marker => map.removeLayer(marker))
      markers = []

      // Get all locations from store
      const locations = moviesStore.allLocations

      // Add markers for each location
      locations.forEach(location => {
        if (location.lat && location.lng) {
          const marker = L.marker([location.lat, location.lng])
            .addTo(map)
            .bindPopup(`
              <div class="map-popup">
                <h3>${location.name}</h3>
                <p><strong>Address:</strong> ${location.address}</p>
                <p><strong>Movies filmed here:</strong></p>
                <ul>
                  ${location.movies.map(movie => `<li><strong>${movie.title}</strong> (${movie.year}) - ${movie.director}</li>`).join('')}
                </ul>
              </div>
            `)
          
          markers.push(marker)
        }
      })
    }

    const showAllLocations = () => {
      if (!map || markers.length === 0) return
      
      const group = new L.featureGroup(markers)
      map.fitBounds(group.getBounds().pad(0.1))
    }

    const centerOnSF = () => {
      if (!map) return
      map.setView([37.7749, -122.4194], 12)
    }

    const toggleSatellite = () => {
      if (!map) return

      // Remove current layer
      map.removeLayer(currentLayer)

      if (isSatellite.value) {
        // Switch to street view
        currentLayer = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
          attribution: '© OpenStreetMap contributors'
        }).addTo(map)
        isSatellite.value = false
      } else {
        // Switch to satellite view
        currentLayer = L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}', {
          attribution: '© Esri'
        }).addTo(map)
        isSatellite.value = true
      }
    }

    return {
      showAllLocations,
      centerOnSF,
      toggleSatellite,
      isSatellite
    }
  }
}
</script>

<style scoped>
.movie-map-container {
  position: relative;
  width: 100%;
  height: 500px;
  border-radius: 15px;
  overflow: hidden;
  box-shadow: 0 5px 20px rgba(0,0,0,0.1);
}

.map {
  width: 100%;
  height: 100%;
}

.map-controls {
  position: absolute;
  top: 10px;
  right: 10px;
  z-index: 1000;
  display: flex;
  gap: 10px;
  flex-wrap: wrap;
}

.control-btn {
  background: white;
  border: none;
  padding: 8px 16px;
  border-radius: 5px;
  box-shadow: 0 2px 5px rgba(0,0,0,0.2);
  cursor: pointer;
  font-size: 14px;
  font-weight: 500;
  transition: all 0.3s ease;
  white-space: nowrap;
}

.control-btn:hover {
  background: #f8f9fa;
  transform: translateY(-1px);
  box-shadow: 0 4px 8px rgba(0,0,0,0.3);
}

:deep(.map-popup) {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  max-width: 300px;
}

:deep(.map-popup h3) {
  margin: 0 0 10px 0;
  color: #333;
  font-size: 16px;
  font-weight: 600;
}

:deep(.map-popup p) {
  margin: 5px 0;
  color: #666;
  font-size: 14px;
}

:deep(.map-popup ul) {
  margin: 5px 0;
  padding-left: 15px;
  max-height: 150px;
  overflow-y: auto;
}

:deep(.map-popup li) {
  margin: 3px 0;
  color: #333;
  font-size: 13px;
  line-height: 1.4;
}

@media (max-width: 768px) {
  .movie-map-container {
    height: 400px;
  }
  
  .map-controls {
    flex-direction: column;
    gap: 5px;
    right: 5px;
    top: 5px;
  }
  
  .control-btn {
    padding: 6px 12px;
    font-size: 12px;
  }
}
</style>
