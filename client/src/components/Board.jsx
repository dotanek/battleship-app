import React, { Component } from "react";
import styled from "styled-components";
import fieldType from "../models/fieldType";

import Water from "../assets/images/water.jpg";
import Hitmark from "../assets/images/hitmark.png";
import Missmark from "../assets/images/missmark.png";

import Ship from "./Ship";

const Container = styled.div`
  position: relative;
  display: grid;
  width: 100%;
  grid-template-columns: 10% 10% 10% 10% 10% 10% 10% 10% 10% 10%;
  grid-template-rows: 10% 10% 10% 10% 10% 10% 10% 10% 10% 10%;
  aspect-ratio: 1;
  background-image: url("${Water}");
  border: 1px solid rgba(255, 255, 255, 0.5);
`;

const Labels = styled.div`
  font-size: 2vw;
  position: absolute;
  left: 0;
  top: 0;
  width: 100%;
  height: 6%;
  display: flex;

  @media (min-width: 600px) {
    font-size: 1.2vw;
  }

  @media (min-width: 1080px) {
    font-size: 12px;
  }
`;

const LabelsX = styled(Labels)`
  transform: translateY(-100%);
`;

const LabelsY = styled(Labels)`
  transform-origin: top left;
  transform: rotate(90deg);

  & > div {
    transform: rotate(-90deg);
  }
`;

const Label = styled.div`
  flex: 1;
  display: flex;
  font-weight: bold;
  color: white;
  height: 100%;
  align-items: center;
  justify-content: center;
  text-shadow: 1px 2px 3px rgba(0, 0, 0, 0.8);
`;

const Field = styled.div`
  position: relative;
  border: 1px solid rgba(255, 255, 255, 0.1);
  transition: 0.2s ease-in-out;
`;

// background-color: ${(props) => getFieldColor(props.type)};

const Mark = styled.div`
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 9999;
  background-image: url("${(props) => getMarkImage(props.type)}");
  background-position: center;
  background-size: cover;
`;

const getMarkImage = (type) => {
  switch (type) {
    case fieldType.MISS:
      return Missmark;
    case fieldType.HIT:
      return Hitmark;
    default:
      return "";
  }
};

class Board extends Component {
  state = {};

  renderFields() {
    if (this.props.fields && this.props.ships) {
      return this.props.fields.map((f, i) => {
        const x = Math.floor(i % 10);
        const y = Math.floor(i / 10);

        const ship = this.props.ships.find(
          (s) => s.position.x === x && s.position.y === y
        );

        return (
          <Field key={i} type={f}>
            {this.props.shipsVisible && ship && <Ship ship={ship} />}
            <Mark type={f} />
          </Field>
        );
      });
    }

    return [...Array(100).keys()].map((k) => <Field key={k}></Field>);
  }

  renderLabels(horizontal) {
    const values = [...Array(10).keys()];

    if (horizontal) {
      return (
        <LabelsX>
          {values.map((v) => (
            <Label key={v}>{v + 1}</Label>
          ))}
        </LabelsX>
      );
    } else {
      return (
        <LabelsY>
          {values.map((v) => (
            <Label key={v}>{String.fromCharCode(v + 65)}</Label>
          ))}
        </LabelsY>
      );
    }
  }

  render() {
    return (
      <Container>
        {this.renderFields()}
        {this.renderLabels(true)}
        {this.renderLabels(false)}
      </Container>
    );
  }
}

export default Board;
