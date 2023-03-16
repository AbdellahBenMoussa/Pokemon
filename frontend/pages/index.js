import Head from 'next/head'
import Container from 'react-bootstrap/Container'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import Card from 'react-bootstrap/Card'

export default function Home() {
  const pages = ['categories', 'countries', 'owners']#
  useEffect(() => {
    fetch
  
    return () => {
      second
    }
  }, [third])
  
  return (
    <>
      <Head></Head>
      <main className="home__page-body">
        <Container className="pt-5">
          <h1 className="text-center text-light">Welcome To Pokemon App</h1>

          <Row className="mt-5">
            {pages.map(p => (
              <Col>
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
        </Container>
      </main>
    </>
  )
}
