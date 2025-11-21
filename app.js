// Lotto Application
// Generates lotto numbers based on frequency data from CSV file

class LottoApp {
    constructor() {
        this.frequencyData = [];
        this.generateBtn = document.getElementById('generateBtn');
        this.loadDataBtn = document.getElementById('loadDataBtn');
        this.resultsDiv = document.getElementById('results');
        this.dataStatus = document.getElementById('dataStatus');
        this.frequencyTable = document.getElementById('frequencyTable');
        
        this.init();
    }
    
    init() {
        this.generateBtn.addEventListener('click', () => this.generateNumbers());
        this.loadDataBtn.addEventListener('click', () => this.loadFrequencyData());
        this.generateBtn.disabled = true;
    }
    
    async loadFrequencyData() {
        try {
            this.updateStatus('Loading frequency data...', 'info');
            
            const response = await fetch('data/lotto_frequency.csv');
            if (!response.ok) {
                throw new Error('Failed to load CSV file');
            }
            
            const csvText = await response.text();
            this.parseCSV(csvText);
            
            this.updateStatus(`Successfully loaded ${this.frequencyData.length} numbers`, 'success');
            this.generateBtn.disabled = false;
            this.displayFrequencyTable();
            
        } catch (error) {
            this.updateStatus(`Error: ${error.message}`, 'error');
            console.error('Error loading data:', error);
        }
    }
    
    parseCSV(csvText) {
        const lines = csvText.trim().split('\n');
        // Skip header row
        const dataLines = lines.slice(1);
        
        this.frequencyData = dataLines.map(line => {
            const [number, frequency] = line.split(',').map(item => item.trim());
            return {
                number: parseInt(number),
                frequency: parseInt(frequency)
            };
        }).filter(item => !isNaN(item.number) && !isNaN(item.frequency));
        
        console.log('Loaded frequency data:', this.frequencyData);
    }
    
    displayFrequencyTable() {
        this.frequencyTable.innerHTML = '';
        
        // Sort by frequency (descending) for better visualization
        const sortedData = [...this.frequencyData].sort((a, b) => b.frequency - a.frequency);
        
        sortedData.forEach(item => {
            const div = document.createElement('div');
            div.className = 'frequency-item';
            div.innerHTML = `
                <div class="number-label">${item.number}</div>
                <div class="frequency-value">${item.frequency}x</div>
            `;
            this.frequencyTable.appendChild(div);
        });
    }
    
    generateNumbers() {
        if (this.frequencyData.length === 0) {
            this.updateStatus('Please load frequency data first', 'error');
            return;
        }
        
        // Generate a row of lotto numbers (typically 6 numbers)
        const numbersPerRow = 6;
        const selectedNumbers = this.selectWeightedNumbers(numbersPerRow);
        
        this.displayResults(selectedNumbers);
    }
    
    selectWeightedNumbers(count) {
        // Create a weighted pool of numbers based on frequency
        const weightedPool = [];
        
        this.frequencyData.forEach(item => {
            // Add the number to the pool 'frequency' times
            for (let i = 0; i < item.frequency; i++) {
                weightedPool.push(item.number);
            }
        });
        
        // Select unique random numbers from the weighted pool
        const selected = new Set();
        let attempts = 0;
        const maxAttempts = 1000;
        
        while (selected.size < count && attempts < maxAttempts) {
            const randomIndex = Math.floor(Math.random() * weightedPool.length);
            const number = weightedPool[randomIndex];
            selected.add(number);
            attempts++;
        }
        
        // Convert to array and sort
        return Array.from(selected).sort((a, b) => a - b);
    }
    
    displayResults(numbers) {
        const rowDiv = document.createElement('div');
        rowDiv.className = 'lotto-row';
        
        const timestamp = new Date().toLocaleTimeString();
        rowDiv.innerHTML = `
            <h3>Generated at ${timestamp}</h3>
            <div class="numbers">
                ${numbers.map(num => `<div class="number">${num}</div>`).join('')}
            </div>
        `;
        
        // Insert at the top
        this.resultsDiv.insertBefore(rowDiv, this.resultsDiv.firstChild);
        
        // Keep only last 5 results
        while (this.resultsDiv.children.length > 5) {
            this.resultsDiv.removeChild(this.resultsDiv.lastChild);
        }
    }
    
    updateStatus(message, type) {
        this.dataStatus.textContent = message;
        this.dataStatus.className = 'status';
        if (type === 'success') {
            this.dataStatus.classList.add('success');
        } else if (type === 'error') {
            this.dataStatus.classList.add('error');
        }
    }
}

// Initialize the app when the page loads
document.addEventListener('DOMContentLoaded', () => {
    new LottoApp();
});
