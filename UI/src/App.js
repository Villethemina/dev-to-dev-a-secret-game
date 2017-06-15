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
      </div>
    );
  }
}

export default App;
