import { ClassNameMap } from "@material-ui/core/styles/withStyles";

import useStyles from "./styles";

export const useService = () => {
  const classes: ClassNameMap<"root"> = useStyles();

  return { classes };
};
