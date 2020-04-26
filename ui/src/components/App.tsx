import {createMuiTheme, MuiThemeProvider} from "@material-ui/core";
import {Route, Switch} from "react-router-dom";
import React from "react";
import StartPage from "./start/StartPage";
import VotePage from "./voting/VotePage";

const theme = createMuiTheme({
    typography: {
        fontFamily: "Montserrat, sans-serif"
    }
});

export const App: () => any = () => {
    return <MuiThemeProvider theme={theme}>
        <Switch>
            <Route exact
                   path="/"
                   component={StartPage}/>
            <Route
                exact
                path="/session/:id"
                component={VotePage}
            />
        </Switch>
    </MuiThemeProvider>
}