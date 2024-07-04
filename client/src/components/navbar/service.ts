import { ClassNameMap } from "@material-ui/core/styles/withStyles";

import useStyles from "./styles";

export const useService = (): { classes: ClassNameMap<"root"> } => {
  const classes: ClassNameMap<"root"> = useStyles();

  return { classes };
};
