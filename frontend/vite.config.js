import { defineConfig, loadEnv } from 'vite'
import react from '@vitejs/plugin-react'

// loadEnv permite acceder a las variables del archivo .env según el modo
// (development, production, etc.) antes de que Vite las inyecte.
export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), '')
  return {
    plugins: [react()],
    define: {
      'import.meta.env.VITE_API_URL': JSON.stringify(env.VITE_API_URL || 'http://localhost:5204')
    },
    test: { // Configuración de Vitest para utilizar el framework de testeo.
      globals: true,
      environment: 'jsdom',
      setupFiles: './src/setupTests.js'
    }
  }
})