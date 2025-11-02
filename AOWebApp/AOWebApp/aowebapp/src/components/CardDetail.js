import { useState, useEffect } from "react"
import { Link } from "react-router-dom";

const CardDetail = ({ }) => {
    const [cardData, setState] = useState(); // set to empty object
    const [itemId, setItemId] = useState(1);

    useEffect(() => {
        console.log("useEffect");
        fetch(`http://localhost:5156/api/ItemsWebAPI/${itemId}`)
            .then(response => response.json())
            .then(data => setState(data))
            .catch(err => {
                console.log(err);
            })
    }, [itemId])

    return (
        <div className="card mb-2" style={{ width: 18 + 'rem' }}>
            <img className="card-img-top" src={cardData.itemImage} alt={"Image of " + carData.itemName} />
            <div className="card-body">
                <h5 className="card-title">{cardData.itemName}</h5>
                <p className="card-text">{cardData.itemDescription}</p>
                <p className="card-text">{cardData.itemCost}</p>
                <Link to="/Products" className="btn btn-primary">Back to Products</Link>
            </div>
        </div>
    )
}