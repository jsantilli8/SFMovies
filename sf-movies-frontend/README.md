# 🎬 SF Movies Frontend

A modern Vue.js 3 application showcasing movies filmed in San Francisco with interactive location mapping and advanced filtering capabilities.

## ✨ Features

- **🎬 Movie Database**: Browse iconic movies filmed in San Francisco with detailed information
- **🔍 Advanced Filtering**: Search by title, director, year, genre, and cast members
- **🗺️ Interactive Map**: Click on markers to see which movies were filmed at each location
- **📍 Location Explorer**: Search and filter filming locations with detailed descriptions
- **📱 Responsive Design**: Beautiful UI that works on all devices
- **⚡ Modern Tech Stack**: Vue 3, Pinia, Vue Router, Vite, Leaflet

## 🚀 Quick Start

### Prerequisites
- Node.js 16+ 
- npm or yarn

### Installation

1. **Clone the repository**
```bash
git clone <repository-url>
cd sf-movies-frontend
```

2. **Install dependencies**
```bash
npm install
```

3. **Start development server**
```bash
npm run dev
```

4. **Open your browser**
Navigate to `http://localhost:3000`

## 🛠️ Available Scripts

- `npm run dev` - Start development server
- `npm run build` - Build for production
- `npm run preview` - Preview production build

## 📁 Project Structure

```
src/
├── api/              # API services and endpoints
├── assets/           # Static assets (images, fonts, etc.)
├── components/       # Reusable Vue components
│   └── MovieMap.vue  # Interactive map component
├── router/           # Vue Router configuration
├── store/            # Pinia state management
│   └── movies.js     # Movies store with sample data
├── views/            # Page components
│   ├── Home.vue      # Landing page
│   ├── Movies.vue    # Movies listing with advanced filters
│   └── Locations.vue # Filming locations explorer with map
├── App.vue           # Root component
└── main.js           # Application entry point
```

## 🎯 Sample Movies Included

- **Bullitt** (1968) - Peter Yates - Action
- **Dirty Harry** (1971) - Don Siegel - Crime  
- **The Rock** (1996) - Michael Bay - Action
- **Mrs. Doubtfire** (1993) - Chris Columbus - Comedy
- **Vertigo** (1958) - Alfred Hitchcock - Thriller
- **The Princess Diaries** (2001) - Garry Marshall - Comedy

## 🗺️ Featured Locations

- Russian Hill
- Lombard Street
- City Hall
- Alcatraz Island
- Golden Gate Bridge
- Steiner Street
- Golden Gate Park
- Mission Dolores
- Palace of Fine Arts

## 🎨 Design Features

- **Modern Gradient Design**: Beautiful color schemes throughout
- **Card-based Layout**: Clean, organized content presentation
- **Hover Effects**: Interactive elements with smooth transitions
- **Mobile-first**: Responsive design for all screen sizes
- **Interactive Map**: Clickable markers with movie information
- **Advanced Filters**: Multiple filtering and sorting options
- **Accessibility**: Semantic HTML and proper contrast ratios

## 🔧 Technologies Used

- **Vue 3** - Progressive JavaScript framework
- **Pinia** - State management
- **Vue Router** - Client-side routing
- **Vite** - Fast build tool and dev server
- **Leaflet** - Interactive maps
- **CSS3** - Modern styling with Flexbox and Grid

## 🎮 Key Functionalities

### Movies Page
- **Search**: By title, director, or cast members
- **Filter**: By year, director, genre
- **Sort**: By title, year, or rating
- **Detailed Info**: Cast, locations, ratings, descriptions

### Locations Page
- **Interactive Map**: Click markers to see movie information
- **Map Controls**: Show all locations, center on SF, satellite view
- **Location Search**: Search by name or address
- **Movie Filter**: Filter locations by specific movies
- **Detailed Cards**: Location descriptions and movie lists

### Home Page
- **Welcome Section**: Introduction to the app
- **Feature Cards**: Overview of main functionalities
- **Navigation**: Easy access to all sections

## 📱 Browser Support

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- San Francisco Film Commission for location data
- Movie database information from various sources
- Vue.js community for excellent documentation
- Leaflet community for mapping capabilities

---

**Made with ❤️ for San Francisco movie lovers**
