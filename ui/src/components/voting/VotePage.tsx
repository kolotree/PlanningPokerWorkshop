import React, {useCallback} from "react";
import {Box, Button, lighten, TextField} from "@material-ui/core";
import fibonacci_series from "../../utils/fibonacciGenerator";
import {union} from "lodash";
import {makeStyles} from "@material-ui/core/styles";

const useStyles = makeStyles({
    root: {
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        flexDirection: "column"
    },
    voteBtns: {
        display: "flex",
        justifyContent: "flex-start",
        flexWrap: "wrap",
        marginTop: "50px",
        width: "50%"
    },
    button: {
        marginLeft: "20px",
        marginRight: "20px",
        marginTop: "20px",
        padding: "10px 30px",
        width: "70px"
    },
    selectedButton: {
        opacity: .7
    },
    actionButtonsContainer: {
        marginTop: "50px",
        display: "flex",
        justifyContent: "space-between",
        alignItems: "center",
        width: "30%"
    },
    actionButtons: {
        padding: "20px 50px",
    }
});

const VotePage: React.FunctionComponent = () => {

    const voteSequence = union(fibonacci_series(10), []).filter(v => v != 0);
    const classes = useStyles();
    const [selectedVote, setSelectedVote] = React.useState(0);

    const selectVote = (value: number) => {
        setSelectedVote(value);
    }

    return (
        <Box className={classes.root} component="div" height="100%">
            <TextField label="Name"/>
            <div className={classes.voteBtns}>
                {
                    voteSequence.map(value => {
                        return <Button key={value} onClick={() => selectVote(value)}
                                       className={classes.button} style={{opacity: selectedVote === value ? .7 : 1}} variant="contained" color="primary">
                            {value}
                        </Button>
                    })
                }
            </div>
            <div className={classes.actionButtonsContainer}>
                <Button className={classes.actionButtons} variant="contained" color="secondary">
                    Vote
                </Button>
                <Button className={classes.actionButtons} variant="contained" color="secondary">
                    Clear all
                </Button>
            </div>
        </Box>
    )
};

export default VotePage;