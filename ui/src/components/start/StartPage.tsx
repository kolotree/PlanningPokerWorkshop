import React from "react";
import {Button, Box} from "@material-ui/core";
import {makeStyles} from "@material-ui/core/styles";

const useStyles = makeStyles({
    root: {
        display: "flex",
        justifyContent: "center",
        alignItems: "center"
    },
    button: {
        padding: "50px 100px"
    }
});

const StartPage: React.FunctionComponent = () => {
    const classes = useStyles();

    return (
        <Box className={classes.root} component="div" height="100%">
            <Button className={classes.button} variant="contained" color="primary" disableElevation>
                Start poker session
            </Button>
        </Box>
    );
};

export default StartPage;