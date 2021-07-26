import React, { Component } from "react";
import styled from "styled-components";
import config from "../config/config";
import axios from "axios";

import Board from "./Board";

import TableBackground from "../assets/images/table.jpg";

const Container = styled.div`
  width: 92%;
  padding: 10%;
  display: flex;
  flex-direction: column;

  @media screen and (min-width: 1080px) {
    width: 1000px;
  }
`;

const Display = styled.div`
  display: flex;
  flex-direction: row;
  width: 90%;
  padding: 5%;
  border-radius: 2%;
`;

/*
background-image: url("${TableBackground}");
  background-position: center;
  background-size: cover;
  box-shadow: 2px 5px 10px rgba(0, 0, 0, 0.5);
  */

const BoardSeparator = styled.div`
  width: 4%;
`;

class Game extends Component {
  state = {};

  componentDidMount() {
    const url = `${config.api.url}/game/simulate`;
    axios
      .get(url)
      .then((res) => {
        this.setState({ game: res.data });
        console.log(res.data);
      })
      .catch((err) => {
        console.log(err?.response);
      });
  }

  render() {
    return (
      <Container>
        <Display>
          <Board />
          <BoardSeparator />
          <Board />
        </Display>
      </Container>
    );
  }
}

export default Game;
