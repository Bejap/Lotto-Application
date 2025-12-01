// Lotto Application
// Generates lotto numbers based on frequency data from API

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
            
            const response = await fetch('/api/lotto/load-data', {
                method: 'POST'
            });
            
            if (!response.ok) {
                const error = await response.json();
                throw new Error(error.message || 'Failed to load frequency data');
            }
            
            const data = await response.json();
            
            if (!data.success) {
                throw new Error(data.message);
            }
            
            this.frequencyData = data.frequencyData;
            this.updateStatus(data.message, 'success');
            this.generateBtn.disabled = false;
            this.displayFrequencyTable();
            
        } catch (error) {
            this.updateStatus(`Error: ${error.message}`, 'error');
            console.error('Error loading data:', error);
        }
    }
    
    displayFrequencyTable() {
        this.frequencyTable.innerHTML = '';
        
        // Data is already sorted by frequency from the API
        this.frequencyData.forEach(item => {
            const div = document.createElement('div');
            div.className = 'frequency-item';
            div.innerHTML = `
                <div class="number-label">${item.number}</div>
                <div class="frequency-value">${item.frequency}x</div>
            `;
            this.frequencyTable.appendChild(div);
        });
    }
    
    async generateNumbers() {
        if (this.frequencyData.length === 0) {
            this.updateStatus('Please load frequency data first', 'error');
            return;
        }
        
        try {
            const response = await fetch('/api/lotto/generate', {
                method: 'POST'
            });
            
            if (!response.ok) {
                const error = await response.json();
                throw new Error(error.message || 'Failed to generate numbers');
            }
            
            const result = await response.json();
            this.displayResults(result.numbers, result.generatedAt);
            
        } catch (error) {
            this.updateStatus(`Error: ${error.message}`, 'error');
            console.error('Error generating numbers:', error);
        }
    }
    
    displayResults(numbers, generatedAt) {
        const rowDiv = document.createElement('div');
        rowDiv.className = 'lotto-row';
        
        const timestamp = new Date(generatedAt).toLocaleTimeString();
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
