<template>
  <div class="movies">
    <div class="movies-header">
      <h2>San Francisco Movies</h2>
      <p>Discover iconic movies filmed in the City by the Bay</p>
    </div>

    <div class="filters">
      <div class="search-box">
        <input 
          v-model="moviesStore.searchQuery"
          type="text" 
          placeholder="Search movies, directors, or actors..."
          class="search-input"
        />
      </div>
      
      <div class="filter-controls">
        <select v-model="moviesStore.selectedYear" class="filter-select">
          <option value="">All Years</option>
          <option v-for="year in moviesStore.uniqueYears" :key="year" :value="year">
            {{ year }}
          </option>
        </select>

        <select v-model="moviesStore.selectedDirector" class="filter-select">
          <option value="">All Directors</option>
          <option v-for="director in moviesStore.uniqueDirectors" :key="director" :value="director">
            {{ director }}
          </option>
        </select>

        <select v-model="moviesStore.selectedGenre" class="filter-select">
          <option value="">All Genres</option>
          <option v-for="genre in moviesStore.uniqueGenres" :key="genre" :value="genre">
            {{ genre }}
          </option>
        </select>

        <select v-model="moviesStore.sortBy" class="filter-select">
          <option value="title">Sort by Title</option>
          <option value="year">Sort by Year</option>
          <option value="rating">Sort by Rating</option>
        </select>

        <button @click="moviesStore.clearFilters()" class="clear-btn">
          Clear Filters
        </button>
      </div>
    </div>

    <div class="movies-grid">
      <div 
        v-for="movie in moviesStore.filteredMovies" 
        :key="movie.id" 
        class="movie-card"
      >
        <div class="movie-header-card">
          <div class="movie-title-section">
            <h3>{{ movie.title }}</h3>
            <div class="movie-meta">
              <span class="movie-year">{{ movie.year }}</span>
              <span class="movie-genre">{{ movie.genre }}</span>
              <span class="movie-rating">⭐ {{ movie.rating }}</span>
            </div>
          </div>
        </div>
        
        <div class="movie-info">
          <p class="movie-director">Directed by {{ movie.director }}</p>
          <p class="movie-description">{{ movie.description }}</p>
          
          <div class="movie-cast">
            <h4>Cast:</h4>
            <div class="cast-tags">
              <span v-for="actor in movie.cast" :key="actor" class="cast-tag">
                {{ actor }}
              </span>
            </div>
          </div>
          
          <div class="movie-locations">
            <h4>Filming Locations:</h4>
            <ul>
              <li v-for="location in movie.locations" :key="location.name">
                <strong>{{ location.name }}</strong><br>
                <small>{{ location.address }}</small>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>

    <div v-if="moviesStore.filteredMovies.length === 0" class="no-results">
      <h3>No movies found</h3>
      <p>Try adjusting your search criteria</p>
    </div>
  </div>
</template>

<script>
import { useMoviesStore } from '../store/movies'

export default {
  name: 'Movies',
  setup() {
    const moviesStore = useMoviesStore()
    return { moviesStore }
  }
}
</script>

<style scoped>
.movies {
  max-width: 1200px;
  margin: 0 auto;
}

.movies-header {
  text-align: center;
  margin-bottom: 3rem;
}

.movies-header h2 {
  font-size: 2.5rem;
  color: #333;
  margin-bottom: 1rem;
}

.movies-header p {
  font-size: 1.2rem;
  color: #666;
}

.filters {
  background: white;
  padding: 2rem;
  border-radius: 15px;
  box-shadow: 0 5px 20px rgba(0,0,0,0.1);
  margin-bottom: 3rem;
}

.search-box {
  margin-bottom: 1.5rem;
}

.search-input {
  width: 100%;
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

.filter-controls {
  display: flex;
  gap: 1rem;
  flex-wrap: wrap;
  align-items: center;
}

.filter-select {
  padding: 8px 12px;
  border: 2px solid #e1e5e9;
  border-radius: 8px;
  background: white;
  font-size: 0.9rem;
}

.clear-btn {
  padding: 8px 16px;
  background: #ff6b6b;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-size: 0.9rem;
  transition: background-color 0.3s ease;
}

.clear-btn:hover {
  background: #ff5252;
}

.movies-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(350px, 1fr));
  gap: 2rem;
}

.movie-card {
  background: white;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
  overflow: hidden;
  transition: box-shadow 0.2s ease;
  border: 1px solid #e9ecef;
}

.movie-card:hover {
  box-shadow: 0 4px 12px rgba(0,0,0,0.15);
}

.movie-header-card {
  background: #f8f9fa;
  border-bottom: 2px solid #667eea;
  padding: 1rem;
}

.movie-title-section h3 {
  font-size: 1.5rem;
  margin: 0 0 0.5rem 0;
  color: #333;
}

.movie-meta {
  display: flex;
  gap: 0.5rem;
  flex-wrap: wrap;
}

.movie-year {
  background: #667eea;
  color: white;
  padding: 2px 8px;
  border-radius: 4px;
  font-size: 0.8rem;
}

.movie-genre {
  background: #28a745;
  color: white;
  padding: 2px 8px;
  border-radius: 4px;
  font-size: 0.8rem;
}

.movie-rating {
  background: #ffc107;
  color: #333;
  padding: 2px 8px;
  border-radius: 4px;
  font-size: 0.8rem;
}

.movie-info {
  padding: 1.5rem;
}


.movie-director {
  color: #666;
  font-size: 0.9rem;
  margin-bottom: 1rem;
  font-style: italic;
}

.movie-description {
  color: #666;
  line-height: 1.6;
  margin-bottom: 1.5rem;
}

.movie-cast h4 {
  color: #333;
  font-size: 1rem;
  margin-bottom: 0.5rem;
}

.cast-tags {
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
  margin-bottom: 1.5rem;
}

.cast-tag {
  background: #6c757d;
  color: white;
  padding: 2px 6px;
  border-radius: 4px;
  font-size: 0.75rem;
}

.movie-locations h4 {
  color: #333;
  font-size: 1rem;
  margin-bottom: 0.5rem;
}

.movie-locations ul {
  list-style: none;
  padding: 0;
}

.movie-locations li {
  margin-bottom: 0.5rem;
  padding: 0.5rem;
  background: #f8f9fa;
  border-radius: 5px;
}

.no-results {
  text-align: center;
  padding: 3rem;
  color: #666;
}

@media (max-width: 768px) {
  .filter-controls {
    flex-direction: column;
    align-items: stretch;
  }
  
  .filter-select,
  .clear-btn {
    width: 100%;
  }
}
</style>
