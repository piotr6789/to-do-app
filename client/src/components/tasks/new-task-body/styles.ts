import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles(() => ({
  smallRoot: {
    minWidth: 200,
    maxWidth: 300,
    margin: "0 auto",
    textAlign: "center",
    backgroundColor: "#D2D4D4",
  },
  title: {
    fontSize: 20,
  },
  smallTextField: {
    marginBottom: 16,
  },
  smallSelect: {
    marginBottom: 16,
  },
}));
export default useStyles;
