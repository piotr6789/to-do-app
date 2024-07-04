import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles(() => ({
  root: {
    minWidth: 300,
    maxWidth: 400,
    margin: "auto",
    textAlign: "center",
    backgroundColor: "#D2D4D4",
    padding: 20,
  },
  title: {
    fontSize: 24,
    marginBottom: 20,
  },
  textField: {
    marginBottom: 10,
  },
  button: {
    marginTop: 10,
  },
}));

export default useStyles;
