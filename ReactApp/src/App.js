import Books from "./Books";
import Borrows from "./Borrows";
import Students from "./Students";

function App() {

  return (
    <div className="App">
      <div className="mainContainer">
        <div className="booksContainer">
          <Books />
        </div>
        <div className="studentContainer">
          <Students />
        </div>
        <div className="borrowContainer">
          <Borrows />
        </div>
      </div>
    </div>
  );

}

export default App;