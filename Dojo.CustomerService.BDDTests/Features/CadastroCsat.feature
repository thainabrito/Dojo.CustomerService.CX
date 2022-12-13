#language: pt-br
Funcionalidade: Createcsat
Criação do CSAT (Customer Satisfaction Score)

@tag1
Cenario: CreateValidCsat
Dado o Id '3f4dfa22-ceb8-4dc1-8c33-516023280c7b'
E o Comment 'comentario'
E o Score '5'
E o ProblemSolved 'true'
Quando criada a avaliacao
Entao retornar o csatId
