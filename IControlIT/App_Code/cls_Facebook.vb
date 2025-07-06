Imports Microsoft.VisualBasic
Imports System.Data

Public Class cls_Facebook
    Public Class FaceBookUser
        Public Property Id() As String
            Get
                Return m_Id
            End Get
            Set(value As String)
                m_Id = value
            End Set
        End Property
        Private m_Id As String
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set(value As String)
                m_Name = value
            End Set
        End Property
        Private m_Name As String
        Public Property UserName() As String
            Get
                Return m_UserName
            End Get
            Set(value As String)
                m_UserName = value
            End Set
        End Property
        Private m_UserName As String
        Public Property PictureUrl() As String
            Get
                Return m_PictureUrl
            End Get
            Set(value As String)
                m_PictureUrl = value
            End Set
        End Property
        Private m_PictureUrl As String
        Public Property Email() As String
            Get
                Return m_Email
            End Get
            Set(value As String)
                m_Email = value
            End Set
        End Property
        Private m_Email As String
    End Class

End Class

