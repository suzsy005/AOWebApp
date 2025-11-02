
function Card(props) {

    return (
        <div className="card col-4 mb-2" style={{ width: 18 + 'rem' }}>
            <img className="card-img-top" src={props.itemImage} alt={"Image of" + props.itemName} />
            <div className="card-body">
                <h5 className="card-title">{props.itemName}</h5>
                <p className="card-text">Some quick example text to build ...</p>
                <a href="#" className="btn btn-primary">Go somewhere</a>
            </div>
        </div>
    )
}

export default Card;