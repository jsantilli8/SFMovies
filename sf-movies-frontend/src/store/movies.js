import { defineStore } from 'pinia'

export const useMoviesStore = defineStore('movies', {
  state: () => ({
    movies: [
      {
        id: 1,
        title: "Bullitt",
        year: 1968,
        director: "Peter Yates",
        description: "A San Francisco police detective becomes obsessed with tracking down the underworld kingpin who killed the witness in his protection.",
        locations: [
          { name: "Russian Hill", address: "Russian Hill, San Francisco, CA", lat: 37.8014, lng: -122.4104 },
          { name: "Lombard Street", address: "Lombard St, San Francisco, CA", lat: 37.8021, lng: -122.4187 }
        ],
        poster: null,
        rating: 7.4,
        genre: "Action",
        cast: ["Steve McQueen", "Jacqueline Bisset", "Robert Vaughn"]
      },
      {
        id: 2,
        title: "Dirty Harry",
        year: 1971,
        director: "Don Siegel",
        description: "A hard-nosed San Francisco police detective is determined to stop a psychopathic killer by any means necessary.",
        locations: [
          { name: "City Hall", address: "1 Dr Carlton B Goodlett Pl, San Francisco, CA", lat: 37.7799, lng: -122.4194 },
          { name: "Kezar Stadium", address: "755 Stanyan St, San Francisco, CA", lat: 37.7694, lng: -122.4462 }
        ],
        poster: null,
        rating: 7.7,
        genre: "Crime",
        cast: ["Clint Eastwood", "Andrew Robinson", "Harry Guardino"]
      },
      {
        id: 3,
        title: "The Rock",
        year: 1996,
        director: "Michael Bay",
        description: "A mild-mannered chemist and an ex-con must lead the counterstrike when a rogue group of military men, led by a renegade general, threaten a nerve gas attack from Alcatraz against San Francisco.",
        locations: [
          { name: "Alcatraz Island", address: "Alcatraz Island, San Francisco, CA", lat: 37.8270, lng: -122.4230 },
          { name: "Golden Gate Bridge", address: "Golden Gate Bridge, San Francisco, CA", lat: 37.8199, lng: -122.4783 }
        ],
        poster: null,
        rating: 7.4,
        genre: "Action",
        cast: ["Sean Connery", "Nicolas Cage", "Ed Harris"]
      },
      {
        id: 4,
        title: "Mrs. Doubtfire",
        year: 1993,
        director: "Chris Columbus",
        description: "After a bitter divorce, an actor disguises himself as a female housekeeper to spend time with his children held in custody by his former wife.",
        locations: [
          { name: "Steiner Street", address: "Steiner St, San Francisco, CA", lat: 37.7749, lng: -122.4194 },
          { name: "Golden Gate Park", address: "Golden Gate Park, San Francisco, CA", lat: 37.7694, lng: -122.4862 }
        ],
        poster: null,
        rating: 7.0,
        genre: "Comedy",
        cast: ["Robin Williams", "Sally Field", "Pierce Brosnan"]
      },
      {
        id: 5,
        title: "Vertigo",
        year: 1958,
        director: "Alfred Hitchcock",
        description: "A former San Francisco police detective juggles wrestling with his personal demons and becoming obsessed with the hauntingly beautiful woman he has been hired to trail.",
        locations: [
          { name: "Mission Dolores", address: "3321 16th St, San Francisco, CA", lat: 37.7599, lng: -122.4269 },
          { name: "Palace of Fine Arts", address: "3301 Lyon St, San Francisco, CA", lat: 37.8029, lng: -122.4484 }
        ],
        poster: null,
        rating: 8.3,
        genre: "Thriller",
        cast: ["James Stewart", "Kim Novak", "Barbara Bel Geddes"]
      },
      {
        id: 6,
        title: "The Princess Diaries",
        year: 2001,
        director: "Garry Marshall",
        description: "A socially awkward but very bright 15-year-old girl being raised by a single mom discovers that she is the princess of a small European country because of the recent death of her long-absent father.",
        locations: [
          { name: "Palace of Fine Arts", address: "3301 Lyon St, San Francisco, CA", lat: 37.8029, lng: -122.4484 },
          { name: "Golden Gate Park", address: "Golden Gate Park, San Francisco, CA", lat: 37.7694, lng: -122.4862 }
        ],
        poster: null,
        rating: 6.3,
        genre: "Comedy",
        cast: ["Anne Hathaway", "Julie Andrews", "Hector Elizondo"]
      }
    ],
    searchQuery: '',
    selectedYear: '',
    selectedDirector: '',
    selectedGenre: '',
    sortBy: 'title'
  }),

  getters: {
    filteredMovies: (state) => {
      let filtered = state.movies

      if (state.searchQuery) {
        filtered = filtered.filter(movie => 
          movie.title.toLowerCase().includes(state.searchQuery.toLowerCase()) ||
          movie.director.toLowerCase().includes(state.searchQuery.toLowerCase()) ||
          movie.year.toString().includes(state.searchQuery) ||
          movie.genre.toLowerCase().includes(state.searchQuery.toLowerCase()) ||
          movie.cast.some(actor => actor.toLowerCase().includes(state.searchQuery.toLowerCase()))
        )
      }

      if (state.selectedYear) {
        filtered = filtered.filter(movie => movie.year.toString() === state.selectedYear.toString())
      }

      if (state.selectedDirector) {
        filtered = filtered.filter(movie => movie.director === state.selectedDirector)
      }

      if (state.selectedGenre) {
        filtered = filtered.filter(movie => movie.genre === state.selectedGenre)
      }

      // Sort movies
      switch (state.sortBy) {
        case 'title':
          return filtered.sort((a, b) => a.title.localeCompare(b.title))
        case 'year':
          return filtered.sort((a, b) => b.year - a.year)
        case 'rating':
          return filtered.sort((a, b) => b.rating - a.rating)
        default:
          return filtered
      }
    },

    uniqueYears: (state) => {
      return [...new Set(state.movies.map(movie => movie.year))].sort((a, b) => b - a)
    },

    uniqueDirectors: (state) => {
      return [...new Set(state.movies.map(movie => movie.director))].sort()
    },

    uniqueGenres: (state) => {
      return [...new Set(state.movies.map(movie => movie.genre))].sort()
    },

    allLocations: (state) => {
      const locationMap = new Map()
      
      state.movies.forEach(movie => {
        movie.locations.forEach(location => {
          if (!locationMap.has(location.name)) {
            locationMap.set(location.name, {
              id: location.name.toLowerCase().replace(/\s+/g, '-'),
              name: location.name,
              address: location.address,
              lat: location.lat,
              lng: location.lng,
              movies: []
            })
          }
          locationMap.get(location.name).movies.push({
            title: movie.title,
            year: movie.year,
            director: movie.director
          })
        })
      })
      
      return Array.from(locationMap.values())
    }
  },

  actions: {
    setSearchQuery(query) {
      this.searchQuery = query
    },

    setSelectedYear(year) {
      this.selectedYear = year
    },

    setSelectedDirector(director) {
      this.selectedDirector = director
    },

    setSelectedGenre(genre) {
      this.selectedGenre = genre
    },

    setSortBy(sortBy) {
      this.sortBy = sortBy
    },

    clearFilters() {
      this.searchQuery = ''
      this.selectedYear = ''
      this.selectedDirector = ''
      this.selectedGenre = ''
      this.sortBy = 'title'
    }
  }
})
