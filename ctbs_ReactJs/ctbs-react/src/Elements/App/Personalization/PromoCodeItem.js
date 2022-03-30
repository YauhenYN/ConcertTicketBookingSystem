import { useState } from "react";
import Button from "../../../CommonElements/Button";
import store from "../../../store";
import * as actionCreators from "../../../actionCreators";

function PromoCodeItem(props) {
    const [promoCode, setPromoCode] = useState(props.promoCode);
    return (
        <div className="promoCodeItem">
            <div className="promoCodeLeft">
                {promoCode.uniqueCode}
            </div>
            <div className="simplePromoCode discountCode">
                {promoCode.discount}$
            </div>
            <div className="simplePromoCode">
                {promoCode.isActiveFlag ? <>Активен</> : <>Не Активен</>}
            </div>
            <div className="simplePromoCode">
                {promoCode.leftCount}
            </div>
            <div className="simplePromoCode">
            {promoCode.isActiveFlag ? <Button text = "Деактивировать" onClick = {deActivatePromoCode(promoCode, setPromoCode)}/> : <Button text = "Активировать" onClick = {activatePromoCode(promoCode, setPromoCode)}/>}
            </div>
        </div>
    );
}

function activatePromoCode(promoCode, setPromoCode){
    return function action(){
        store.dispatch(actionCreators.ActivatePromoCodeInListActionCreator(promoCode.promoCodeId)).then(() => {
            setPromoCode({
                ...promoCode,
                isActiveFlag: true
            })
        });
    }
}
function deActivatePromoCode(promoCode, setPromoCode){
    return function action(){
        store.dispatch(actionCreators.DeactivatePromoCodeInListActionCreator(promoCode.promoCodeId)).then(() => {
            setPromoCode({
                ...promoCode,
                isActiveFlag: false
            })
        });
    }
}
export default PromoCodeItem;