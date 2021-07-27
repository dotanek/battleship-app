import React, { Component } from "react";
import styled from "styled-components";

import Game from "./components/Game";

const Container = styled.div`
  display: flex;
  width: 100%;
  min-height: 100vh;
  align-items: center;
  justify-content: center;
`;

class App extends Component {
  state = {};
  render() {
    return (
      <Container>
        <Game></Game>
      </Container>
    );
  }
}

export default App;
