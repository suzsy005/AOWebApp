//import logo from './logo.svg';
import './App.css';
import Card from './components/Card'

function App() {
  return (
    <div className="App container">
        <div className="bg-light py-1 mb-2">
            <h2 className="text-center">
                Example Application
            </h2>
        </div>
        <div className="row justify-content-center">
              <Card itemId="1"
                    itemName="test records1"
                    itemDescription="test record 1 Desc"
                    itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg"
                    itemCost="15.00"
              />
        </div>
    </div>
  );
}

export default App;
