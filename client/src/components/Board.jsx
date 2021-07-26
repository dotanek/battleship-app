import React, { Component } from "react";
import styled from "styled-components";

import Water from "../assets/images/water.jpg";

const Container = styled.div`
  position: relative;
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  flex: 1;
  aspect-ratio: 1;
  background-image: url("${Water}");
  border: 1px solid rgba(255, 255, 255, 0.5);
`;

const Labels = styled.div`
  position: absolute;
  left: 0;
  top: 0;
  width: 100%;
  height: 6%;
  display: flex;
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
  width: calc(10% - 2px);
  height: calc(10% - 2px);
  border: 1px solid rgba(255, 255, 255, 0.1);
`;

class Board extends Component {
  state = {};

  constructor(props) {
    super(props);

    this.state.fields = [...Array(100).keys()];
  }

  renderFields() {
    return this.state.fields.map((f) => <Field></Field>);
  }

  renderLabels(horizontal) {
    const values = [...Array(10).keys()];

    if (horizontal) {
      return (
        <LabelsX>
          {values.map((v) => (
            <Label>{v + 1}</Label>
          ))}
        </LabelsX>
      );
    } else {
      return (
        <LabelsY>
          {values.map((v) => (
            <Label>{String.fromCharCode(v + 65)}</Label>
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
