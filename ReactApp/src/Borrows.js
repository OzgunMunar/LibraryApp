import React, { useEffect, useState } from 'react'
import { useRef } from 'react'

function Borrow() {

    const [borrowTableLine, setBorrowTableLine] = useState()

    //Get Borrow Data
    useEffect(() => {
        
        fetch('https://localhost:7251/api/borrowinfo')
        .then(res => res.json())
        .then(
            dataRow => setBorrowTableLine(dataRow.map(row => {
              console.log(row)
            return (
                <tr key={row.borrowId}>
                    <td>{row.bookName}</td>
                    <td>{row.studentName}</td>
                    <td>{row.borrowDate}</td>
                </tr>
            )

        }))
        )
        .catch(function (error) {
          console.log(error);
        })

    },[])

    return (
      <>
          <div className='topSection'>
            <i>Book Borrows</i>
          </div>
          <table>
              <thead>
                <tr>
                  <th>Book Title</th>
                  <th>Student Full Name</th>
                  <th>Borrow Date</th>
                </tr>
              </thead>
              <tbody>
                    {borrowTableLine}
              </tbody>
          </table>
      </>
    )

}

export default Borrow