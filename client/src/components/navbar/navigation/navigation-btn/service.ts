import { NavigateFunction, useNavigate } from "react-router-dom";

import { ClassNameMap } from "@material-ui/core/styles/withStyles";

import useStyles from "./styles";

export const useService = (route: string) => {
  const classes: ClassNameMap<"root"> = useStyles();
  const navigate: NavigateFunction = useNavigate();

  const clickHandler = (): void => navigate(route);

  return { classes, clickHandler };
};
