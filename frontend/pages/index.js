import { Container, Row, Col, Card } from 'react-bootstrap'

export default function Home() {
  const pages = ['categories', 'countries', 'owners']

  return (
    <>
      <h1 className="text-center text-light">Welcome To Pokemon App</h1>

      <Row className="mt-5">
        {pages.map(p => (
          <Col key={p}>
            <a
              href={`/${p}`}
              className="text-decoration-none shadow text-dark text-center"
            >
              <Card>
                <Card.Body>
                  <img src={`/${p}.png`} alt={p} />
                  <Card.Title className="text-capitalize">{p}</Card.Title>
                </Card.Body>
              </Card>
            </a>
          </Col>
        ))}
      </Row>
    </>
  )
}
