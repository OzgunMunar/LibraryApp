import React, { useEffect, useState } from 'react'
import { useRef } from 'react'

function Students() {

    const [studentTableLine, setStudentTableLine] = useState()
    const [newStudentInfo, setNewStudentInfo] = useState({
            "studentId": 0,
            "firstName": "",
            "lastName": "",
            "grade": 0,
            "borrowInfos": [],
    })
    const [editMode, setEditMode] = useState(false)

    const firstNameRef = useRef()
    const lastNameRef = useRef()
    const gradeRef = useRef()

    //Get Student Data
    useEffect(() => {
        
        fetch('https://localhost:7251/api/student')
        .then(res => res.json())
        .then(
            dataRow => setStudentTableLine(dataRow.map(row => {
              // console.log(row)
            return (
                <tr key={row.studentId}>
                    <td>{row.firstName}</td>
                    <td>{row.lastName}</td>
                    <td>{row.grade}</td>
                    <td>
                        <button onClick={()=>deleteRecord(row.studentId)}>Delete</button>
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
        fetch('https://localhost:7251/api/student/' + id, { method: 'DELETE'})
        .then(function () {
            window.location.reload(true)
        })
        .catch(function (err) {
            console.log(err)
        })
    }

    function addRecord() {
        
        fetch('https://localhost:7251/api/student', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { 
                "studentId": newStudentInfo.bookId,
                "firstName": newStudentInfo.firstName,
                "lastName": newStudentInfo.lastName,
                "grade": newStudentInfo.grade,
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
        
        fetch(`https://localhost:7251/api/student/${newStudentInfo.studentId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        data: {
            "id": newStudentInfo.bookId
        },
        body: JSON.stringify(
            {   
              "studentId": newStudentInfo.bookId,
              "firstName": newStudentInfo.firstName,
              "lastName": newStudentInfo.lastName,
              "grade": newStudentInfo.grade,
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

    function editModeFunc(StudentRow) {

      console.log(StudentRow)

      firstNameRef.current.value = StudentRow.firstName
      lastNameRef.current.value = StudentRow.lastName
      gradeRef.current.value = StudentRow.grade

      firstNameRef.current.focus()

      setEditMode(value => !value)
      setNewStudentInfo(info => {

          return {
            
            ...info,
            "studentId": StudentRow.studentId,
            "firstName": firstNameRef.current.value,
            "lastName": lastNameRef.current.value,
            "grade": gradeRef.current.value,
            "borrowInfos": []

          }

      })

    }

    function setValues(event) {
        
        const { name, value } = event.target

        setNewStudentInfo(info => {
            return{
                ...info,
                [name]:value
            }
        })

    }

    return (
      <>
          <div className='topSection'>
            <i>Students</i>
            <input type='text' id='firstName' name="firstName" 
            value={newStudentInfo.firstName} onChange={setValues}
            ref={firstNameRef}/>

            <input type='text' id='lastName' name="lastName" 
            value={newStudentInfo.lastName} onChange={setValues}
            ref={lastNameRef}/>

            <input type='text' id='grade' name="grade"  
            value={newStudentInfo.grade} onChange={setValues}
            ref={gradeRef}/>

            <button onClick={() => {addRecord()}}> - Add - </button>
            {(editMode === true ? <button onClick={() => {editRecord()}}> - SaveChanges - </button> : <p></p>)}
          </div>
          <table>
              <thead>
                <tr>
                  <th>First Name</th>
                  <th>Last Name</th>
                  <th>Grade</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                    {studentTableLine}
              </tbody>
          </table>
      </>
    )

}

export default Students