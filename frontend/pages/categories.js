import { useEffect, useState, useContext, createContext } from 'react'
import { Table, Button, Form } from 'react-bootstrap'
import NewCategoryModal from '@/components/Modals/NewCategoryModal'

export const categoriesContext = createContext()

// Create a provider component
const CategoriesProvider = ({ children }) => {
  const [categories, setCategories] = useState([])
  return (
    <categoriesContext.Provider value={{ categories, setCategories }}>
      {children}
    </categoriesContext.Provider>
  )
}

export default function Categories() {
  function addNewCategory() {
    if (newCategory === '') return
    const formData = new FormData()
    formData.append('Name', newCategory)

    fetch('https://localhost:7192/api/Category', {
      method: 'POST',
      body: formData,
    })
    setCategories(prev => [...prev, { name: newCategory }])
    setNewCategory('')
  }

  return (
    <CategoriesProvider>
      <div>
        <h1 className="mb-5 text-light text-center">Pokemon Categories</h1>
        <NewCategoryModal />
        {/* delete modal */}
        {/* update modal */}
        <CategoriesTable />
      </div>
    </CategoriesProvider>
  )
}

function CategoriesTable() {
  const { categories, setCategories } = useContext(categoriesContext)

  useEffect(() => {
    fetch('https://localhost:7192/api/Category/GetAllCategories')
      .then(res => res.json())
      .then(res => setCategories(res))
  }, [])

  function deleteCategory(id) {
    fetch(`https://localhost:7192/api/Category/${id}`, {
      method: 'DELETE',
    }).then(res => {
      console.log(res.ok)
      setCategories(prev => prev.filter(item => item.id !== id))
    })
  }

  return (
    <>
      <Table striped bordered variant="light" hover>
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {categories.map((cat, i) => (
            <tr key={cat.name}>
              <td>{cat.id}</td>
              <td>{cat.name}</td>
              <th className="w-25">
                <Button variant="secondary" className="me-3">
                  Edit
                </Button>
                <Button variant="danger" onClick={() => deleteCategory(cat.id)}>
                  Delete
                </Button>
              </th>
            </tr>
          ))}
        </tbody>
      </Table>
    </>
  )
}
