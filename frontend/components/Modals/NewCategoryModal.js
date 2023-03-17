import { Form, Button, Modal } from 'react-bootstrap'
import { useContext, useState } from 'react'
import { categoriesContext } from '@/pages/categories'

export default function NewCategoryModal() {
  const [show, setShow] = useState(false)
  const [newCategory, setNewCategory] = useState('')
  const { setCategories } = useContext(categoriesContext)

  const handleClose = () => setShow(false)
  const handleShow = () => setShow(true)
  const handleSubmit = e => {
    if (newCategory === '') return
    const formData = new FormData()
    formData.append('name', newCategory)
    e.preventDefault()
    fetch('https://localhost:7192/api/Category', {
      method: 'POST',
      body: formData,
    }).then(res => {
      console.log(res.ok)
      setCategories(prev => [...prev, { name: newCategory }])
      setNewCategory('')
      handleClose()
    })
  }

  return (
    <>
      <Button variant="primary" onClick={handleShow} className="mb-3">
        New Category
      </Button>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>New Category</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form onSubmit={e => handleSubmit(e)}>
            <Form.Group className="mb-3">
              <Form.Label>Name</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter category name"
                value={newCategory}
                onChange={e => setNewCategory(e.target.value)}
              />
            </Form.Group>
            <Button variant="primary" type="submit">
              Add
            </Button>
          </Form>
        </Modal.Body>
      </Modal>
    </>
  )
}
