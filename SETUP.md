# Lotto Application - Setup Guide

This guide provides comprehensive instructions for setting up the Lotto Application development environment.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Development Environment](#development-environment)
- [Running the Application](#running-the-application)
- [Testing](#testing)
- [Building for Production](#building-for-production)
- [Git Workflow](#git-workflow)
- [Troubleshooting](#troubleshooting)

## Prerequisites

Before setting up the Lotto Application, ensure you have the following installed on your system:

- **Git** (version 2.x or higher)
- **Programming Language Runtime** (to be specified based on implementation choice):
  - Python 3.8+ (recommended for data analysis capabilities)
  - Node.js 16+ and npm/yarn (for web-based implementation)
  - Java 11+ (for enterprise-grade implementation)
- **Database** (optional, for data persistence):
  - SQLite (lightweight, recommended for development)
  - PostgreSQL 12+ (recommended for production)
  - MySQL 8+ (alternative option)
- **Code Editor/IDE** (recommended):
  - Visual Studio Code
  - PyCharm (for Python implementation)
  - IntelliJ IDEA (for Java implementation)

## Installation

### 1. Clone the Repository

```bash
git clone https://github.com/Bejap/Lotto-Application.git
cd Lotto-Application
```

### 2. Install Dependencies

#### For Python Implementation:

```bash
# Create a virtual environment
python -m venv venv

# Activate the virtual environment
# On Windows:
venv\Scripts\activate
# On macOS/Linux:
source venv/bin/activate

# Install required packages
pip install -r requirements.txt
```

#### For Node.js Implementation:

```bash
# Install dependencies
npm install
# or
yarn install
```

#### For Java Implementation:

```bash
# Using Maven
mvn clean install

# Using Gradle
gradle build
```

## Configuration

### Environment Variables

Create a `.env` file in the root directory with the following variables:

```env
# Application Settings
APP_ENV=development
DEBUG=true

# Database Configuration
DB_TYPE=sqlite
DB_PATH=./data/lotto.db
# For PostgreSQL/MySQL:
# DB_HOST=localhost
# DB_PORT=5432
# DB_NAME=lotto_app
# DB_USER=your_username
# DB_PASSWORD=your_password

# Lottery Configuration
LOTTERY_TYPE=standard
NUMBERS_COUNT=6
NUMBER_RANGE_MIN=1
NUMBER_RANGE_MAX=49

# Data Source (for historical lottery data)
# Replace with actual data source URL
DATA_SOURCE_URL=https://api.example.com/lottery-data
DATA_SOURCE_API_KEY=your_api_key_here
```

### Configuration File

Create or modify `config.yaml` (or `config.json`) for application-specific settings:

```yaml
lottery:
  type: "standard"
  numbers_per_draw: 6
  number_range:
    min: 1
    max: 49

analysis:
  historical_data_limit: 1000  # Number of past draws to analyze
  hot_number_threshold: 0.15   # Frequency threshold for hot numbers
  cold_number_threshold: 0.05  # Frequency threshold for cold numbers

generation:
  default_rows: 5              # Default number of rows to generate
  algorithms:
    - frequency_based
    - pattern_based
    - random_weighted
```

## Development Environment

### Setting Up the Database

#### SQLite (Development):

```bash
# Database will be created automatically on first run
# Or manually create the schema:
sqlite3 data/lotto.db < schema.sql
```

#### PostgreSQL (Production):

```bash
# Create the database
createdb lotto_app

# Run migrations
# Python (using Alembic):
alembic upgrade head

# Node.js (using Knex/Sequelize):
npm run migrate

# Java (using Flyway):
mvn flyway:migrate
```

### Importing Historical Data

```bash
# Download sample historical data (replace URL with actual data source)
curl -o data/historical_draws.csv https://example.com/lottery-data.csv

# Import the data
# Python:
python scripts/import_data.py data/historical_draws.csv

# Node.js:
npm run import-data data/historical_draws.csv

# Java:
java -jar import-tool.jar data/historical_draws.csv
```

## Running the Application

### Development Mode

#### Command Line Interface:

```bash
# Python
python main.py

# Node.js
npm run dev

# Java
mvn exec:java -Dexec.mainClass="com.lotto.Main"
```

#### Web Interface (if implemented):

```bash
# Python (Flask/Django)
flask run
# or
python manage.py runserver

# Node.js (Express)
npm start

# Java (Spring Boot)
mvn spring-boot:run
```

The application will be available at `http://localhost:8080` (or configured port).

### Using the Application

```bash
# Generate lottery numbers
./lotto generate --rows 5

# Analyze historical data
./lotto analyze --draws 100

# Check number frequency
./lotto frequency --number 7

# View hot and cold numbers
./lotto stats
```

## Testing

### Running Tests

```bash
# Python
pytest tests/
# With coverage:
pytest --cov=src tests/

# Node.js
npm test
# With coverage:
npm run test:coverage

# Java
mvn test
# With coverage:
mvn test jacoco:report
```

### Test Structure

```
tests/
├── unit/              # Unit tests for individual components
├── integration/       # Integration tests for combined functionality
├── data/             # Test data and fixtures
└── fixtures/         # Reusable test fixtures
```

### Running Specific Tests

```bash
# Python - specific test file
pytest tests/unit/test_number_generator.py

# Node.js - specific test suite
npm test -- --grep "NumberGenerator"

# Java - specific test class
mvn test -Dtest=NumberGeneratorTest
```

## Building for Production

### Creating a Production Build

#### Python:

```bash
# Create distribution package
python setup.py sdist bdist_wheel

# Or using PyInstaller for standalone executable
pyinstaller --onefile main.py
```

#### Node.js:

```bash
# Build production assets
npm run build

# Create Docker image
docker build -t lotto-app:latest .
```

#### Java:

```bash
# Create JAR file
mvn clean package

# Create Docker image
docker build -t lotto-app:latest .
```

### Deployment

```bash
# Using Docker
docker run -d -p 8080:8080 --env-file .env.production lotto-app:latest

# Using Docker Compose
docker-compose up -d
```

## Git Workflow

### Branch Structure

This repository uses the following branching strategy:

- **main** - Production-ready code. All releases are tagged from this branch.
- **develop** - Integration branch for features. All feature branches merge here first.
- **feature/** - Feature branches created from `develop` for new features.
- **bugfix/** - Bug fix branches for addressing issues.
- **hotfix/** - Critical fixes that need immediate deployment to production.

### Creating Feature Branches

```bash
# Create and switch to a new feature branch
git checkout develop
git pull origin develop
git checkout -b feature/your-feature-name

# Work on your feature...
git add .
git commit -m "Add feature description"

# Push to remote
git push -u origin feature/your-feature-name
```

### Merging Changes

```bash
# Update your feature branch with latest develop
git checkout feature/your-feature-name
git fetch origin
git merge origin/develop

# Create a pull request on GitHub
# After PR approval, merge to develop
git checkout develop
git pull origin develop
git merge feature/your-feature-name
git push origin develop
```

### Branch Protection Rules (Recommended)

Configure the following protection rules for `main` and `develop` branches:

1. Require pull request reviews before merging
2. Require status checks to pass before merging
3. Require branches to be up to date before merging
4. Include administrators in restrictions
5. Require linear history (optional)

## Troubleshooting

### Common Issues

#### Database Connection Errors

```bash
# Check database status
# PostgreSQL:
pg_isready -h localhost

# MySQL:
mysqladmin ping -h localhost

# Verify connection string in .env file
```

#### Missing Dependencies

```bash
# Python - reinstall dependencies
pip install -r requirements.txt --force-reinstall

# Node.js - clear cache and reinstall
npm cache clean --force
rm -rf node_modules package-lock.json
npm install

# Java - clean and rebuild
mvn clean install -U
```

#### Import Data Issues

```bash
# Verify data format
head -n 5 data/historical_draws.csv

# Check for file encoding issues
file -i data/historical_draws.csv

# Validate CSV structure
python -c "import csv; list(csv.reader(open('data/historical_draws.csv')))"
```

#### Port Already in Use

```bash
# Find process using the port (e.g., 8080)
# Linux/macOS:
lsof -i :8080
# Windows:
netstat -ano | findstr :8080

# Kill the process or use a different port in configuration
```

### Getting Help

If you encounter issues not covered in this guide:

1. Check the [GitHub Issues](https://github.com/Bejap/Lotto-Application/issues) for existing problems
2. Review the application logs in `logs/` directory
3. Enable debug mode by setting `DEBUG=true` in `.env`
4. Create a new issue with detailed error messages and steps to reproduce

## Additional Resources

- [README.md](README.md) - Project overview and features
- [CONTRIBUTING.md](CONTRIBUTING.md) - Contribution guidelines (if available)
- [API Documentation](docs/API.md) - API reference (if applicable)
- [Architecture Documentation](docs/ARCHITECTURE.md) - System design (if available)

## Next Steps

After completing the setup:

1. Review the [README.md](README.md) to understand the application features
2. Explore the codebase structure
3. Run the test suite to ensure everything is working
4. Try generating some lottery numbers using sample data
5. Read the contributing guidelines before making changes
6. Join the development team discussions

---

For questions or support, please open an issue on GitHub or contact the maintainers.
