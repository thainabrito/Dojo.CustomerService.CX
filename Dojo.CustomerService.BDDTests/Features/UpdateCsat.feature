#language: pt-br
Funcionalidade: UpdateCSAT
Atualização do CSAT (Customer Satisfaction Score)

@csat
@csat-update
@csat-update-UpdateValidScore
@aceite
@regressivo
Cenario:UpdateValidScore
    Dado o Id '3f4dfa22-ceb8-4dc1-8c33-516023280c7c'
    E o Score '5'
    Quando atualizada a avaliacao
    Entao retornar o csatId

@csat
@csat-update
@csat-update-UpdateInvalidScore
@aceite
@regressivo
Cenario:UpdateInvalidScore
    Dado o Id '3f4dfa22-ceb8-4dc1-8c33-516023280c7d'
    E o Score '6'
    Quando atualizada a avaliacao
    E nota não for válida
    Entao retornar mensagem de nota invalida

@csat
@csat-update
@csat-update-UpdateValidComment
@aceite
@regressivo
    Cenario:UpdateValidComment
    Dado o Id '3f4dfa22-ceb8-4dc1-8c33-516023280c7e'
    E o Comment 'comentario'
    Quando atualizada a avaliacao
    Entao retornar o csatId

@csat
@csat-update
@csat-update-UpdateInvalidComment
@aceite
@regressivo
    Cenario:UpdateInvalidComment
    Dado o Id '3f4dfa22-ceb8-4dc1-8c33-516023280c7f'
    E o Comment ' '
    Quando atualizada a avaliacao
    E o comentario for nulo ou vazio
    Entao retornar a mensagem de comentario deve ser preenchido

@csat
@csat-update
@csat-update-UpdateValidFCR
@aceite
@regressivo
    Cenario:UpdateValidFCR
    Dado o Id '3f4dfa22-ceb8-4dc1-8c33-516023280c7g'
    E o ProblemSolved 'true'
    Quando atualizada a avaliacao
    Entao retornar o csatId 
