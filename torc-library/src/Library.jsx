import React, { useState, useEffect } from "react";
import "./Library.css";
import emptyLstImge from "./assets/emptylst.jpeg";

function Library() {
  const data = fetchData();
  const [bookList, setBookList] = useState(data ? data : []);
  const [newItem, searchBookBy] = useState("");

  async function fetchData() {
    try {
      const response = await axios.get("http://localhost:5136/books");
      setBookList(response.data);
    } catch (error) {
      console.error(error);
    }
  }

  useEffect(() => {
    fetchData();
  }, []);

  function searchBook(e) {
    if (e && e.preventDefault) {
      e.preventDefault();
    }
    if (!newItem) {
      return;
    }

    setBookList([...bookist, { text: newItem, isDone: false }]);
    searchBookBy("");
    document.getElementById("input-srch").focus();
  }

  function changeValueDone(index) {
    const laux = [...bookList];
    laux[index].isDone = !laux[index].isDone;
    setBookList(laux);
  }

  function deleteItem(index) {
    if (taskList[index]) {
      const laux = [...bookList];
      laux.splice(index, 1);
      setBookList(laux);
    }
  }

  function deleteAll() {
    setBookList([]);
  }

  return (
    <div>
      <h1>Task List</h1>
      <form onSubmit={searchBook}>
        <span> Search By</span>
        <input
          id="input-srch"
          type="select"
          value={newItem}
          onChange={(e) => searchBookBy(e.target.value)}
          placeholder="Type the value to search"
        />
        <button className="add" type="submit" onClick={() => searchBook()}>
          Search
        </button>
      </form>
      <div className="taskList">
        <div>
          {taskList.length < 1 ? (
            <img className="emptyLstImg" width={100} src={emptyLstImge} />
          ) : (
            taskList.map((item, index) => (
              <div key={index} className="item">
                <span>{item.title}</span>
                <span>{item.isbn}</span>
                <span>{item.name}</span>
              </div>
            ))
          )}
        </div>
        {taskList.length > 0 && (
          <button onClick={() => deleteAll()} className="delAll">
            Del All
          </button>
        )}
      </div>
    </div>
  );
}

export default Library;
