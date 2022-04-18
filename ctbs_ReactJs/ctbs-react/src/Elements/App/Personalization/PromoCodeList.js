import './ActionList.css';
import PromoCodeItem from "./PromoCodeItem";
import "./PromoCodeList.css";

function PromoCodeList(props) {
    return (
        <div className = "outOfListTable">
        <div className = "ListTable">
            <div className = "ListHeader">
                <div className="promoCodeLeft">Код</div>
                <div className="simplePromoCode discountCode">Скидка</div>
                <div className="simplePromoCode">Статус</div>
                <div className="simplePromoCode">Осталось</div>
                <div className="simplePromoCode aboveButton"></div>
            </div>
            <div className="ListItemFirst"/>
            {props.promoCodeList.map(promoCode => {
                return <PromoCodeItem key={promoCode.uniqueCode} promoCode = {promoCode} />
            })}
        </div>
        </div>
    );
}
export default PromoCodeList;