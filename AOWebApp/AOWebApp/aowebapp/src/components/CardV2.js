﻿
function CardV2({ itemId, itemName, itemDescription, itemCost, itemImage }) {

    return (
        <div className="card col-4 mb-2" style={{ width: 18 + 'rem' }}>
            <img className="card-img-top" src={itemImage} alt={"Image of" + itemName} />
            <div className="card-body">
                <h5 className="card-title">{itemName}</h5>
                <p className="card-text">Some quick example text to build ...</p>
                <a href="#" className="btn btn-primary">Go somewhere</a>
            </div>
        </div>
    )
}

export default CardV2;