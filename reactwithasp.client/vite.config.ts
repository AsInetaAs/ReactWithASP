/*import tailwindcss from '@tailwindss/vite';

export default defineConfig({
    plugins: [plugin(), tailwindcss()],
});
   */
import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// Tailwind veikia per postcss, nereikia tailwind plugin Vite

export default defineConfig({
    plugins: [react()],
});
