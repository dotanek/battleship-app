import React, { Component } from "react";
import styled from "styled-components";
import config from "../config/config";
import axios from "axios";
import fieldType from "../models/fieldType";
import playerNumber from "../models/playerNumber";

import Board from "./Board";

const Container = styled.div`
  width: 92%;
  padding: 10%;
  display: flex;
  flex-direction: column;
  gap: 20px;
  align-items: center;

  @media screen and (min-width: 1080px) {
    width: 1000px;
    gap: 40px;
  }
`;

const Logo = styled.h2`
  color: white;
  text-shadow: 2px 4px 5px rgba(0, 0, 0, 0.5);
  text-align: center;
  padding: 0 20px 20px 20px;

  @media (max-width: 600px) {
    padding: 0 5% 5% 5%;
  }
`;

const Display = styled.div`
  display: flex;
  flex-direction: row;
  width: 100%;
  gap: 40px;

  @media (max-width: 600px) {
    flex-direction: column;
    gap: 30px;
  }

  @media (max-width: 400px) {
    flex-direction: column;
    gap: 30px;
  }
`;

const Controls = styled.div`
  width: 100%;
  display: flex;
  flex-direction: row;
  gap: 4%;
  flex-wrap: wrap;

  @media screen and (min-width: 600px) {
    width: 80%;
  }

  & > Button {
    margin-bottom: 4%;
  }
`;

const Button = styled.button`
  outline: none;
  border: none;
  color: white;
  font-family: var(--font);
  flex: 1;
  background-color: #56803f;
  box-shadow: 2px 3px 5px rgba(0, 0, 0, 0.5);
  font-weight: 500;
  padding: 10px;
  transition: 0.2s ease-in-out;

  &:hover {
    cursor: pointer;
    background-color: #70c466;
  }
`;

class Game extends Component {
  state = {
    playing: false,
    shipsVisible: false,
  };

  fetchSimulation = () => {
    const url = `${config.api.url}/game/simulate`;
    axios
      .get(url)
      .then((res) => {
        console.log(fieldType);
        const game = {
          playerOne: {
            ships: res.data.playerOneShips,
            fields: [...Array(100)].map(() => fieldType.EMPTY),
          },
          playerTwo: {
            ships: res.data.playerTwoShips,
            fields: [...Array(100)].map(() => fieldType.EMPTY),
          },
          moves: res.data.moves,
          winner: res.data.moves[res.data.moves.length - 1].player,
        };

        // For keeping track of animation.
        this.gameplay = {
          moves: [...res.data.moves],
        };

        this.setState({ game });

        console.log(res.data);
      })
      .catch((err) => {
        console.log(err?.response);
      });
  };

  componentDidMount = () => {
    this.fetchSimulation();
  };

  updateField = (move, fields) => {
    const index = move.y * 10 + move.x;

    if (move.hit) {
      fields[index] = fieldType.HIT;
    } else {
      fields[index] = fieldType.MISS;
    }
  };

  updateGame = (move) => {
    const game = this.state.game;

    if (!game) {
      return;
    }

    if (move.player === playerNumber.ONE) {
      this.updateField(move, game.playerTwo.fields);
    } else {
      this.updateField(move, game.playerOne.fields);
    }

    this.setState({ game });
  };

  onClickStart = () => {
    // If the game is already being animated we stop the interval and change the flag.
    if (this.state.playing) {
      clearInterval(this.gameplay.interval);
      this.setState({ playing: false });

      // Otherwise we start it.
    } else {
      this.gameplay.interval = setInterval(() => {
        if (this.gameplay.moves.length !== 0) {
          this.updateGame(this.gameplay.moves.shift());
        } else {
          // If there is no more moves in the gameplay move queue we stop the interval.
          alert(
            `Game ended! Winner: ${
              this.state.game.winner === 1 ? "Player one" : "Player two"
            }`
          );
          clearInterval(this.gameplay.interval);
          this.setState({ playing: false });
        }
      }, 200);

      this.setState({ playing: true });
    }
  };

  onClickReset = () => {
    if (this.gameplay.interval) {
      const game = this.state.game;
      game.playerOne.fields = [...Array(100)].map(() => fieldType.EMPTY);
      game.playerTwo.fields = [...Array(100)].map(() => fieldType.EMPTY);

      this.gameplay.moves = [...this.state.game.moves];
      clearInterval(this.gameplay.interval);
      this.setState({ playing: false, game });
    }
  };

  onClickReGenerate = () => {
    clearInterval(this.gameplay.interval);
    this.setState({ playing: false });

    this.fetchSimulation();
  };

  onClickShow = () => {
    this.setState({ shipsVisible: !this.state.shipsVisible });
  };

  render() {
    return (
      <Container>
        <Logo>Battleship simulator</Logo>
        <Display>
          <Board
            fields={this.state.game?.playerOne.fields}
            ships={this.state.game?.playerOne.ships}
            shipsVisible={this.state.shipsVisible}
          />
          <Board
            fields={this.state.game?.playerTwo.fields}
            ships={this.state.game?.playerTwo.ships}
            shipsVisible={this.state.shipsVisible}
          />
        </Display>
        <Controls>
          <Button onClick={this.onClickStart}>
            {this.state.playing ? "Stop" : "Start"}
          </Button>
          <Button onClick={this.onClickReset}>Reset</Button>
          <Button onClick={this.onClickReGenerate}>Re-generete</Button>
          <Button onClick={this.onClickShow}>
            {this.state.shipsVisible ? "Hide ships" : "Show ships"}
          </Button>
        </Controls>
      </Container>
    );
  }
}

export default Game;
