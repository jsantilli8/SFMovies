<template>
  <div id="app">
    <header class="app-header">
      <nav class="navbar">
        <div class="nav-brand">
          <h1>🎬 SF Movies</h1>
        </div>
        <div class="nav-links">
          <router-link to="/" class="nav-link">Home</router-link>
          <router-link to="/movies" class="nav-link">Movies</router-link>
          <router-link to="/locations" class="nav-link">Locations</router-link>
        </div>
      </nav>
    </header>
    
    <main class="app-main">
      <router-view />
    </main>
    
    <footer class="app-footer">
      <p>&copy; SF Movies - Discover San Francisco's Film Locations</p>
    </footer>
  </div>
</template>

<script>
export default {
  name: 'App'
}
</script>

<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  line-height: 1.6;
  color: #333;
  background-color: #f5f5f5;
}

#app {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.app-header {
  background: #343a40;
  color: white;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.navbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1rem 2rem;
  max-width: 1200px;
  margin: 0 auto;
}

.nav-brand h1 {
  font-size: 2rem;
  font-weight: 700;
}

.nav-links {
  display: flex;
  gap: 2rem;
}

.nav-link {
  color: white;
  text-decoration: none;
  font-weight: 500;
  padding: 0.5rem 1rem;
  border-radius: 5px;
  transition: background-color 0.3s ease;
}

.nav-link:hover,
.nav-link.router-link-active {
  background-color: #495057;
}

.app-main {
  flex: 1;
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
  width: 100%;
}

.app-footer {
  background-color: #333;
  color: white;
  text-align: center;
  padding: 1rem;
  margin-top: auto;
}

.app-footer p {
  opacity: 0.8;
}

@media (max-width: 768px) {
  .navbar {
    flex-direction: column;
    gap: 1rem;
    padding: 1rem;
  }
  
  .nav-links {
    gap: 1rem;
  }
  
  .app-main {
    padding: 1rem;
  }
}
</style>
