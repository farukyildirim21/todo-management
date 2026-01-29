# Official KrakenD image
FROM krakend:2.9

# Working directory inside container
WORKDIR /etc/krakend

# Copy KrakenD config
COPY krakend.json /etc/krakend/krakend.json

# Expose gateway port
EXPOSE 8081

# Run KrakenD
CMD ["krakend", "run", "-c", "/etc/krakend/krakend.json"]
