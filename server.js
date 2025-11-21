// Simple HTTP server for the Lotto Application
const http = require('http');
const fs = require('fs');
const path = require('path');

const PORT = process.env.PORT || 8000;

// MIME types for different file extensions
const mimeTypes = {
    '.html': 'text/html',
    '.css': 'text/css',
    '.js': 'text/javascript',
    '.json': 'application/json',
    '.csv': 'text/csv',
    '.png': 'image/png',
    '.jpg': 'image/jpeg',
    '.gif': 'image/gif',
    '.svg': 'image/svg+xml',
    '.ico': 'image/x-icon'
};

const server = http.createServer((req, res) => {
    console.log(`${req.method} ${req.url}`);

    // Parse URL and handle root
    let filePath = '.' + req.url;
    if (filePath === './') {
        filePath = './index.html';
    }

    // Get file extension
    const extname = String(path.extname(filePath)).toLowerCase();
    const contentType = mimeTypes[extname] || 'application/octet-stream';

    // Read and serve the file
    fs.readFile(filePath, (error, content) => {
        if (error) {
            if (error.code === 'ENOENT') {
                // File not found
                res.writeHead(404, { 'Content-Type': 'text/html' });
                res.end('<h1>404 - File Not Found</h1>', 'utf-8');
            } else {
                // Server error
                res.writeHead(500);
                res.end(`Server Error: ${error.code}`, 'utf-8');
            }
        } else {
            // Success - serve the file
            res.writeHead(200, { 'Content-Type': contentType });
            // Only use encoding for text files
            if (contentType.startsWith('text/') || contentType === 'application/json' || contentType === 'application/javascript') {
                res.end(content, 'utf-8');
            } else {
                res.end(content);
            }
        }
    });
});

server.listen(PORT, () => {
    console.log('========================================');
    console.log('🎰 Lotto Application Server Running');
    console.log('========================================');
    console.log(`Server running at http://localhost:${PORT}/`);
    console.log('Press Ctrl+C to stop the server');
    console.log('========================================');
});
