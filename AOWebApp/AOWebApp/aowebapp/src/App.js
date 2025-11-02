import './App.css';
import Card from './components/Card'
import CardV2 from './components/CardV2'
import CardV3 from './components/CardV3'
import CardList from './components/CardListSearch'
import { Link } from "react-router-dom";

function App() {
  return (
    <div className="App container">
        <nav className="navbar navbar-expand-lg navbar-light bg-ligth">
            <div className="container-fluid">
                <Link className="navbar-brand" to="/">AOWebApp</Link>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavAltMarkup">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNalAltMarkup">
                    <div className="navbar-nav">
                        <Link className="nav-link active" to="/Home">Home</Link>
                        <Link className="nav-link" to="/Contact">Contact</Link>
                        <Link className="nav-link" to="/Products">Products</Link>
                    </div>
                </div>
            </div>
        </nav>



        <div className="row justify-content-center">
              <Card
                  itemId="1"
                  itemName="test records1"
                  itemDescription="test record 1 Desc"
                  itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg"
                  itemCost="15.00"
              />
              <CardV2
                  itemId="2"
                  itemName="test records2"
                  itemDescription="test record 2 Desc"
                  itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg"
                  itemCost="10.00"
              />
              <CardV3
                  itemId="3"
                  itemName="test records3"
                  itemDescription="test record 3 Desc"
                  itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg"
                  itemCost="5.00"
              />
              <CardList />

        </div>
    </div>
  );
}

export default App;
