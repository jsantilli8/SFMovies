<template>
  <div class="home">
    <div class="hero-section">
      <h2>Welcome to SF Movies!</h2>
      <p>Discover the most famous movies filmed in San Francisco</p>
      
      <div class="cta-buttons">
        <router-link to="/movies" class="btn btn-primary">
          Browse Movies
        </router-link>
        <router-link to="/locations" class="btn btn-secondary">
          Explore Locations
        </router-link>
      </div>
    </div>
    
    <div class="features">
      <div class="feature-card">
        <h3>🎬 Movies</h3>
        <p>Discover iconic movies filmed in San Francisco</p>
      </div>
      
      <div class="feature-card">
        <h3>📍 Locations</h3>
        <p>Explore real filming locations from your favorite movies</p>
      </div>
      
      <div class="feature-card">
        <h3>🗺️ Interactive Map</h3>
        <p>Navigate through an interactive map of filming locations</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Home'
}
</script>

<style scoped>
.home {
  text-align: center;
}

.hero-section {
  background: white;
  padding: 3rem 2rem;
  border-radius: 15px;
  box-shadow: 0 5px 20px rgba(0,0,0,0.1);
  margin-bottom: 3rem;
}

.hero-section h2 {
  font-size: 2.5rem;
  color: #333;
  margin-bottom: 1rem;
  font-weight: 600;
}

.hero-section p {
  font-size: 1.2rem;
  color: #666;
  margin-bottom: 2rem;
}

.cta-buttons {
  display: flex;
  gap: 1rem;
  justify-content: center;
  flex-wrap: wrap;
}

.btn {
  padding: 12px 24px;
  border-radius: 8px;
  text-decoration: none;
  font-weight: 600;
  transition: all 0.3s ease;
  display: inline-block;
}

.btn-primary {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
}

.btn-primary:hover {
  transform: translateY(-2px);
  box-shadow: 0 5px 15px rgba(102, 126, 234, 0.4);
}

.btn-secondary {
  background: white;
  color: #667eea;
  border: 2px solid #667eea;
}

.btn-secondary:hover {
  background: #667eea;
  color: white;
  transform: translateY(-2px);
}

.features {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  gap: 2rem;
  margin-top: 2rem;
}

.feature-card {
  background: white;
  padding: 2rem;
  border-radius: 15px;
  box-shadow: 0 5px 20px rgba(0,0,0,0.1);
  transition: transform 0.3s ease;
}

.feature-card:hover {
  transform: translateY(-5px);
}

.feature-card h3 {
  font-size: 1.5rem;
  color: #333;
  margin-bottom: 1rem;
}

.feature-card p {
  color: #666;
  line-height: 1.6;
}

@media (max-width: 768px) {
  .hero-section h2 {
    font-size: 2rem;
  }
  
  .cta-buttons {
    flex-direction: column;
    align-items: center;
  }
  
  .btn {
    width: 200px;
  }
}
</style>
