import { Box, Heading, Text, VStack } from "@chakra-ui/react";

interface Article {
    title: string;
    author: string;
    content: string;
}

interface ArticlesProps {
    articles: Article[];
}

const Articles: React.FC<ArticlesProps> = ({ articles }) => {
    return (
        <Box
            display="flex"
            justifyContent="center"
            alignItems="center"
            flexDirection="column"
            width="100vw"
            bg="#EDD4B2"
            px={4}
        >
            <VStack spacing={6} align="stretch" width="100%" maxW="800px">
                {articles.map((article, index) => (
                    <Box
                        key={index}
                        p={6}
                        borderWidth="1px"
                        borderRadius="lg"
                        bg="#D0A98F"
                        shadow="md"
                        width="100%"
                    >
                        <Heading size={index === 0 ? "lg" : "md"} color="black">
                            {article.title}
                        </Heading>
                        <Text fontWeight="bold" mt={2} color="black">
                            {article.author}
                        </Text>
                        <Text mt={2} color="black">
                            {index === 0
                                ? article.content.slice(0, 200)
                                : article.content.slice(0, 50)}
                            ...
                        </Text>
                    </Box>
                ))}
            </VStack>
        </Box>
    );
};

export default Articles;