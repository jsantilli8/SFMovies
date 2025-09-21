<template>
  <div class="locations">
    <div class="locations-header">
      <h2>Filming Locations</h2>
      <p>Explore the real places where your favorite movies were filmed</p>
    </div>

    
    <div class="map-section">
      <h3>🗺️ Interactive Map</h3>
      <p>Click on the markers to see which movies were filmed at each location</p>
      <MovieMap />
    </div>

   
    <div class="search-section">
      <h3>🔍 Search Locations</h3>
      <div class="search-controls">
        <input 
          v-model="locationSearchQuery"
          type="text" 
          placeholder="Search locations..."
          class="search-input"
        />
        <select v-model="selectedMovie" class="filter-select">
          <option value="">All Movies</option>
          <option v-for="movie in moviesStore.movies" :key="movie.id" :value="movie.title">
            {{ movie.title }} ({{ movie.year }})
          </option>
        </select>
      </div>
    </div>

    <!-- Locations Grid -->
    <div class="locations-section">
      <h3>📍 Location Details</h3>
      <div class="locations-grid">
        <div 
          v-for="location in filteredLocations" 
          :key="location.id" 
          class="location-card"
          @click="centerOnLocation(location)"
        >
          <div class="location-header">
            <h3>{{ location.name }}</h3>
            <div class="location-badge">{{ location.movies.length }} movies</div>
          </div>
          
          <div class="location-info">
            <p class="location-address">{{ location.address }}</p>
            <p class="location-description">{{ getLocationDescription(location.name) }}</p>
            
            <div class="movies-filmed">
              <h4>Movies Filmed Here:</h4>
              <div class="movie-tags">
                <span 
                  v-for="movie in location.movies" 
                  :key="movie.title" 
                  class="movie-tag"
                >
                  {{ movie.title }} ({{ movie.year }})
                </span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { useMoviesStore } from '../store/movies'
import { computed, ref } from 'vue'
import MovieMap from '../components/MovieMap.vue'

export default {
  name: 'Locations',
  components: {
    MovieMap
  },
  setup() {
    const moviesStore = useMoviesStore()
    const locationSearchQuery = ref('')
    const selectedMovie = ref('')

    const filteredLocations = computed(() => {
      let locations = moviesStore.allLocations

      if (locationSearchQuery.value) {
        locations = locations.filter(location => 
          location.name.toLowerCase().includes(locationSearchQuery.value.toLowerCase()) ||
          location.address.toLowerCase().includes(locationSearchQuery.value.toLowerCase())
        )
      }

      if (selectedMovie.value) {
        locations = locations.filter(location => 
          location.movies.some(movie => movie.title === selectedMovie.value)
        )
      }

      return locations
    })

    const getLocationDescription = (locationName) => {
      const descriptions = {
        'Russian Hill': 'A neighborhood known for its steep streets and beautiful views of the bay.',
        'Lombard Street': 'The famous "crookedest street in the world" with eight hairpin turns.',
        'City Hall': 'The seat of government for the City and County of San Francisco.',
        'Kezar Stadium': 'A multi-purpose stadium in Golden Gate Park.',
        'Alcatraz Island': 'Former federal prison located on an island in San Francisco Bay.',
        'Golden Gate Bridge': 'The iconic suspension bridge connecting San Francisco to Marin County.',
        'Steiner Street': 'A residential street in the Castro District.',
        'Golden Gate Park': 'A large urban park with museums, gardens, and recreational facilities.',
        'Mission Dolores': 'The oldest surviving structure in San Francisco.',
        'Palace of Fine Arts': 'A monumental structure originally constructed for the 1915 Panama-Pacific Exposition.'
      }
      return descriptions[locationName] || 'A famous filming location in San Francisco.'
    }

    const getLocationImage = (locationName) => {
      // Return null to hide images
      return null
    }

    const centerOnLocation = (location) => {
      // This would center the map on the selected location
      // Implementation would require access to the map instance
      console.log('Centering on:', location.name)
    }

    return { 
      moviesStore,
      locationSearchQuery,
      selectedMovie,
      filteredLocations,
      getLocationDescription,
      getLocationImage,
      centerOnLocation
    }
  }
}
</script>

<style scoped>
.locations {
  max-width: 1200px;
  margin: 0 auto;
}

.locations-header {
  text-align: center;
  margin-bottom: 3rem;
}

.locations-header h2 {
  font-size: 2.5rem;
  color: #333;
  margin-bottom: 1rem;
}

.locations-header p {
  font-size: 1.2rem;
  color: #666;
}

.map-section {
  background: white;
  padding: 2rem;
  border-radius: 15px;
  box-shadow: 0 5px 20px rgba(0,0,0,0.1);
  margin-bottom: 3rem;
}

.map-section h3 {
  font-size: 1.8rem;
  color: #333;
  margin-bottom: 0.5rem;
  text-align: center;
}

.map-section p {
  text-align: center;
  color: #666;
  margin-bottom: 2rem;
}

.search-section {
  background: white;
  padding: 2rem;
  border-radius: 15px;
  box-shadow: 0 5px 20px rgba(0,0,0,0.1);
  margin-bottom: 3rem;
}

.search-section h3 {
  font-size: 1.8rem;
  color: #333;
  margin-bottom: 1.5rem;
  text-align: center;
}

.search-controls {
  display: flex;
  gap: 1rem;
  flex-wrap: wrap;
  justify-content: center;
}

.search-input {
  flex: 1;
  min-width: 250px;
  padding: 12px 16px;
  border: 2px solid #e1e5e9;
  border-radius: 8px;
  font-size: 1rem;
  transition: border-color 0.3s ease;
}

.search-input:focus {
  outline: none;
  border-color: #667eea;
}

.filter-select {
  padding: 12px 16px;
  border: 2px solid #e1e5e9;
  border-radius: 8px;
  background: white;
  font-size: 1rem;
  min-width: 200px;
}

.locations-section {
  margin-top: 2rem;
}

.locations-section h3 {
  font-size: 1.8rem;
  color: #333;
  margin-bottom: 2rem;
  text-align: center;
}

.locations-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(400px, 1fr));
  gap: 2rem;
}

.location-card {
  background: white;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
  overflow: hidden;
  transition: box-shadow 0.2s ease;
  cursor: pointer;
  border: 1px solid #e9ecef;
}

.location-card:hover {
  box-shadow: 0 4px 12px rgba(0,0,0,0.15);
}

.location-header {
  background: #f8f9fa;
  border-bottom: 2px solid #28a745;
  padding: 1rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.location-header h3 {
  font-size: 1.3rem;
  margin: 0;
  color: #333;
}

.location-badge {
  background: #28a745;
  color: white;
  padding: 2px 8px;
  border-radius: 4px;
  font-size: 0.8rem;
}

.location-info {
  padding: 1.5rem;
}


.location-address {
  color: #666;
  font-size: 0.9rem;
  margin-bottom: 1rem;
}

.location-description {
  color: #666;
  line-height: 1.6;
  margin-bottom: 1.5rem;
}

.movies-filmed h4 {
  color: #333;
  font-size: 1rem;
  margin-bottom: 0.5rem;
}

.movie-tags {
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
}

.movie-tag {
  background: #007bff;
  color: white;
  padding: 2px 6px;
  border-radius: 4px;
  font-size: 0.75rem;
}

@media (max-width: 768px) {
  .locations-grid {
    grid-template-columns: 1fr;
  }
  
  .search-controls {
    flex-direction: column;
  }
  
  .search-input,
  .filter-select {
    min-width: 100%;
  }
}
</style>
