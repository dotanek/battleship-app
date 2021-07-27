import React, { Component } from "react";
import styled from "styled-components";

import direction from "../models/direction";
import shipType from "../models/shipType";

import Carrier from "../assets/images/ship5.png";
import Battleship from "../assets/images/ship4.png";
import Destroyer from "../assets/images/ship3.png";
import Submarine from "../assets/images/ship3.png";
import PatrolBoat from "../assets/images/ship2.png";

// Top and left -1px because of 1px border the field has.
const Container = styled.div`
  position: absolute;
  width: ${(props) => getShipWidth(props.length)};
  height: calc(100% + 2px);
  transform-origin: left top;
  transform: ${(props) => getShipTransform(props.direction, props.length)};
  background-image: url("${(props) => getShipImage(props.type)}");
  background-position: center;
  background-size: contain;
  top: -1px;
  left: -1px;
  border-radius: 10px;
`;

const getShipWidth = (length) => {
  return `calc(${100 * length}% + ${2 * length}px)`;
};

const getShipTransform = (d, l) => {
  const blockLength = (1 / l) * 100;

  switch (d) {
    case direction.UP:
      return `rotate(-90deg) translateX(-${blockLength}%)`;
    case direction.RIGHT:
      return "rotate(0deg)";
    case direction.DOWN:
      return "rotate(90deg) translateY(-100%)";
    case direction.LEFT:
      return `rotate(-180deg) translateX(-${blockLength}%) translateY(-100%)`;
    default:
      return `rotate(0deg)`;
  }
};

const getShipImage = (type) => {
  switch (type) {
    case shipType.CARRIER:
      return Carrier;
    case shipType.BATTLESHIP:
      return Battleship;
    case shipType.DESTROYER:
      return Destroyer;
    case shipType.SUBMARINE:
      return Submarine;
    case shipType.PATROL_BOAT:
      return PatrolBoat;
    default:
      return "";
  }
};

class Ship extends Component {
  state = {};
  render() {
    return (
      <Container
        direction={this.props.ship.position.direction}
        length={this.props.ship.length}
        type={this.props.ship.shipType}
      />
    );
  }
}

export default Ship;
