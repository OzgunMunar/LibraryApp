import React, { useEffect, useState } from 'react'
import { useRef } from 'react'

function Books() {

    const [booksTableLine, setBooksTableLine] = useState()
    const [newBookInfo, setNewBookInfo] = useState({
            "bookId": 0,
            "bookAuthor": "",
            "bookTitle": "",
            "bookPublisher": "",
            "borrowInfos": [],
    })
    const [editMode, setEditMode] = useState(false)

    const bookAuthorRef = useRef()
    const bookTitleRef = useRef()
    const bookPublisherRef = useRef()

    //Get Books Data
    useEffect(() => {
        
        fetch('https://localhost:7251/api/book')
        .then(res => res.json())
        .then(
            dataRow => setBooksTableLine(dataRow.map(row => {
            return (
                <tr key={row.bookId}>
                    <td>{row.bookAuthor}</td>
                    <td>{row.bookTitle}</td>
                    <td>{row.bookPublisher}</td>
                    <td>
                        <button onClick={()=>deleteRecord(row.bookId)}>Delete</button>
                        <button onClick={()=>editModeFunc(row)}>Edit</button>
                    </td>
                </tr>
            )

        }))
        )
        .catch(function (error) {
          console.log(error);
        })
        
    },[])

    function deleteRecord(id) {
        fetch('https://localhost:7251/api/book/' + id, { method: 'DELETE'})
        .then(function () {
            window.location.reload(true)
        })
        .catch(function (err) {
            console.log(err)
        })
    }

    function addRecord() {
        
        fetch('https://localhost:7251/api/book', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { 
                "bookId": newBookInfo.bookId,
                "bookAuthor": newBookInfo.bookAuthor,
                "bookTitle": newBookInfo.bookTitle,
                "bookPublisher": newBookInfo.bookPublisher,
                "borrowInfos": [],
            }
            )
        })
        .then(function () {
            window.location.reload(true)
        })
        .catch(function (err) {
            console.log(err)
        })

    }

    function editRecord() {
        console.log(newBookInfo)
        fetch(`https://localhost:7251/api/book/${newBookInfo.bookId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        data: {
            "id": newBookInfo.bookId
        },
        body: JSON.stringify(
            {   
                "bookId": newBookInfo.bookId,
                "bookAuthor": newBookInfo.bookAuthor,
                "bookTitle": newBookInfo.bookTitle,
                "bookPublisher": newBookInfo.bookPublisher,
                "borrowInfos": [],
            }
            )
        })
        .then(function () {
            window.location.reload(true)
        })
        .catch(function (err) {
            console.log(err)
        })

        setEditMode(value => !value)

    }

    function editModeFunc(BookRow) {

        bookAuthorRef.current.value = BookRow.bookAuthor
        bookTitleRef.current.value = BookRow.bookTitle
        bookPublisherRef.current.value = BookRow.bookPublisher

        bookAuthorRef.current.focus()

        setEditMode(value => !value)
        setNewBookInfo(info => {

            return {
                ...info,
                "bookId": BookRow.bookId,
                "bookAuthor": bookAuthorRef.current.value,
                "bookTitle": bookTitleRef.current.value,
                "bookPublisher": bookPublisherRef.current.value,
            }

        })

    }

    function setValues(event) {
        
        const { name, value } = event.target

        setNewBookInfo(info => {
            return{
                ...info,
                [name]:value
            }
        })

    }

    return (
      <>
          <div className='topSection'>
            <i>Books</i>
            <input type='text' id='author' name="bookAuthor" 
            value={newBookInfo.BookAuthor} onChange={setValues}
            ref={bookAuthorRef}/>

            <input type='text' id='bookTitle' name="bookTitle" 
            value={newBookInfo.BookTitle} onChange={setValues}
            ref={bookTitleRef}/>

            <input type='text' id='Book Publisher' name="bookPublisher"  
            value={newBookInfo.BookPublisher} onChange={setValues}
            ref={bookPublisherRef}/>

            <button onClick={() => {addRecord()}}> - Add - </button>
            {(editMode === true ? <button onClick={() => {editRecord()}}> - SaveChanges - </button> : <p></p>)}
          </div>
          <table>
              <thead>
                <tr>
                  <th>Author</th>
                  <th>Book Title</th>
                  <th>Book Publisher</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                    {booksTableLine}
              </tbody>
          </table>
      </>
    )

}

export default Books