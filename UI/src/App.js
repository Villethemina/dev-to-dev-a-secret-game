import React, { Component } from 'react';
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
   ],
   enemyGrid: [
    [ ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' ],
    [ ' ', ' ', ' ', 'M', ' ', ' ', 'S', 'S', 'S', ' ' ],
    [ ' ', ' ', ' ', ' ', 'M', ' ', ' ', ' ', ' ', ' ' ],
    [ ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' ],
    [ ' ', ' ', ' ', ' ', ' ', 'X', 'X', ' ', ' ', ' ' ],
    [ ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' ],
    [ ' ', 'ME', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' ],
    [ ' ', ' ', 'ME', ' ', ' ', ' ', ' ', ' ', ' ', ' ' ],
    [ ' ', 'ME', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' ],
    [ ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' ]
  ],
};

  componentDidMount() {
    fetch('https://jsonplaceholder.typicode.com/posts/1').then(data => {
      console.log(data);
    });
  }

  getCellColor(cell) {
    switch (cell) {
      case ' ':
        return '#475EF5'
      case 'X':
      case 'S':
        return '#f00'
      case 'ME':
      case 'M':
        return '#000'
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
          fontFamily="Tahoma"
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

  renderCells(grid) {
    return <g>
      {grid.map((row, rowIndex) =>
      row.map((cell, cellIndex) =>
        cell === ' ' ? null :
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
    )}
    </g>
  }

  renderWater() {
    return (
      <rect
        style={{
          fill: '#475EF5',
          width: 400,
          height: 400,
          x: 40,
          y: 40
        }}
        filter="url(#filter1)"
      />
    );
  }

  renderGrid() {
    return <g>
      {this.state.grid.map((row, rowIndex) =>
      row.map((cell, cellIndex) =>
        <rect
          style={{
            stroke: '#ddd',
            strokeWidth: 1,
            width: 40,
            height: 40,
            x: 40 * cellIndex + 40,
            y: 40 * rowIndex + 40,
            fillOpacity: 0
          }}
        />
      )
    )}
    </g>
  }

  renderBoard(grid, title) {
    return (
      <div className="App-board-container">
        <span className="App-board-title">{title}</span>
        <svg className="App-svg">
          <filter id="filter1">
            <feSpecularLighting result="specOut"
              specularExponent={20} lightingColor="#aaa">
              <fePointLight x={120} y={125} z={150}/>
            </feSpecularLighting>
            <feComposite in="SourceGraphic" in2="specOut"
              operator="arithmetic" k1="0" k2="1" k3="1" k4="0"/>
          </filter>
          <g>
            {this.renderRowHeaders()}
            {this.renderColumnHeaders()}
            {this.renderWater()}
            {this.renderGrid()}
            {this.renderCells(grid)}
          </g>
        </svg>
      </div>
    );
  }

  render() {
    return (
      <div className="App">
        <div className="App-header">
          <div className="ocean">
            <div className="wave"/>
            <div className="wave"/>
          </div>
          <span className="App-header-text">Battleships</span>
        </div>
        <div className="App-boards">
          {this.renderBoard(this.state.grid, 'Your board')}
          {this.renderBoard(this.state.enemyGrid, 'Enemy board')}
        </div>
      </div>
    );
  }
}

export default App;
