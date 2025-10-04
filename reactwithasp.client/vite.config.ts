﻿import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import tailwind from '@tailwindcss/vite'
import path from 'path';

export default defineConfig({
    plugins: [react(), tailwind()],
    resolve: {
        alias: {
            '@': path.resolve(__dirname, './src'),
        },
    },
    server: {
        port: 5173,
        open: true,
        proxy: {
            '/api': {
                target: 'http://localhost:54609',
                changeOrigin: true,
                secure: false,
            },
        },
        fs: {
            strict: false,
        },
    },
});
