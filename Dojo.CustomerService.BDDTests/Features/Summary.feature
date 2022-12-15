#language: pt-br
Funcionalidade: Summary
Sumário de CSAT de um agente

@tag1
Cenário: AgentSummary
Dado o AttendanceEmail '3f4dfa22-ceb8-4dc1-8c33-516023280c7b'
E o InitialDate '2022-05-24'
E o EndDate '2022-05-24'
Quando solicitado o sumario
Então deve retornar a lista de csat
E deve retornar o score
E deve retornar o FCR
