import { useState, useEffect } from 'react'
import Card from "./CardV3"
//import cardData from "../assets/itemData.json"

const CardListNew = ({ }) => {

    const [cardData, setState] = useState([]);

    React.useEffect(() => {
        fetch("http://localhost:5156/api/ItemsWebAPI")
            .then(response => response.json())
            .then(data => setState(data))
            .catch(err => {
                console.log(err);
            });
     }, [])

    return (
        <div className="row">
            {cardData.map((obj) => (
                <Card
                    key={obj.itemId}
                    itemId={obj.itemId}
                    itemName={obj.itemName}
                    itemDescription={obj.itemDescription}
                    itemCost={obj.itemCost}
                    itemImage={obj.itemImage}
                />
            ))}
        </div>
    )
}

export default CardListNew

