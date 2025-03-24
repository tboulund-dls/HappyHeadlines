workspace {
    model {
        DraftService = softwareSystem "DraftService" "Responsible for managing drafts of articles."
        DraftDatabase = softwareSystem "DraftDatabase" "Stores all drafts of articles."
        
        DraftService -> DraftDatabase "Storing a draft"
    }
    
    views {
        systemLandscape {
            include *
            autoLayout lr
        }
        
        styles {
            element "DraftService" {
                shape Hexagon
                background #000000
                color #FFFFFF
            }
            
            element "DraftDatabase" {
                shape Cylinder
                background #000000
                color #FFFFFF
            }
        }
    }
}