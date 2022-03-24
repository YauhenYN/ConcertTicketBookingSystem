import ActionList from "./ActionList";
import * as actionCreators from "../../../actionCreators";
import React, { useEffect, useState } from 'react';
import Loading from "../Loading";
import store from "../../../store";

function Personalization() {
    const [actions, setActions] = useState();
    const [isLoading, setisLoading] = useState(true);
    useEffect(() => {
        store.dispatch(actionCreators.GetActionsActionCreator()).then((result) => {
            setActions(result.data);
            setisLoading(false);
        });
    }, []);
    return (
        <div className="element-common">
            {isLoading === true ? (<Loading/>) : (<ActionList actionList={actions} />)}
        </div>
    );
}

export default Personalization;