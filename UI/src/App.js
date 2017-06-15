import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';

class App extends Component {
  state = {
    grid: [
     [ '1', ' ', '2', '2', '2', '2', ' ', ' ', ' ', ' ' ],
     [ '1', ' ', ' ', 'ME', ' ', ' ', 'S', 'S', 'S', ' ' ],
     [ '1', ' ', ' ', ' ', 'ME', ' ', ' ', ' ', ' ', ' ' ],
     [ '1', ' ', '3', ' ', ' ', ' ', ' ', ' ', ' ', ' ' ],
     [ '1', ' ', '3', ' ', ' ', 'X', 'X', ' ', ' ', ' ' ],
     [ ' ', ' ', '3', ' ', ' ', ' ', ' ', ' ', ' ', ' ' ],
     [ ' ', 'M', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' ],
     [ ' ', ' ', 'M', ' ', '4', '4', '4', ' ', ' ', ' ' ],
     [ ' ', 'M', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '5' ],
     [ ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '5' ]
   ]
};

  componentDidMount() {
    fetch('https://jsonplaceholder.typicode.com/posts/1').then(data => {
      console.log(data);
    });
  }

  renderHeaders(header, orientation) {
    return header.map((letter, index) =>
      <g>
        <rect style={{
          stroke: '#ddd',
          strokeWidth: 1,
          width: 40,
          height: 40,
          x: orientation === "row" ? index * 40 + 40 : 0,
          y: orientation === "column" ? index * 40 + 40 : 0,
          fill: '#fff'
        }}>
        </rect>
        <text
          x={orientation === "row" ? index * 40 + 60 : 20}
          y={orientation === "column" ? index * 40 + 60 : 22}
          fontFamily="Verdana"
          fontSize={20}
          fill="black"
          textAnchor="middle"
          alignmentBaseline="middle"
        >
          {letter}
        </text>
      </g>
    );
  }

  renderRowHeaders() {
    const rowHeaders = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'];
    return this.renderHeaders(rowHeaders, 'row');
  }

  renderColumnHeaders() {
    const rowHeaders = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    return this.renderHeaders(rowHeaders, 'column');
  }

  renderGrid() {
    return this.state.grid.map((row, rowIndex) =>
      row.map((cell, cellIndex) =>
        <rect
          style={{
            stroke: '#ddd',
            strokeWidth: 1,
            width: 40,
            height: 40,
            x: 40 * cellIndex + 40,
            y: 40 * rowIndex + 40,
            fill: this.getCellColor(cell)
          }}
        />
      )
    );
  }

  getCellColor(cell) {
    switch (cell) {
      case ' ':
        return '#00f'
      case 'X':
        return '#ff0'
      case 'S':
        return '#f00'
      case 'M':
        return '#0f0'
      case '1':
      case '2':
      case '3':
      case '4':
      case '5':
        return '#888'
      default:
        return '#fff'
    }
  }

  render() {
    return (
      <div className="App">
        <div className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h2>Welcome to React</h2>
        </div>
        <p className="App-intro">
          To get started, edit <code>src/App.js</code> and save to reload.
        </p>
        <svg className="App-svg">
          {this.renderRowHeaders()}
          {this.renderColumnHeaders()}
          {this.renderGrid()}
        </svg>
      </div>
    );
  }
}

export default App;
